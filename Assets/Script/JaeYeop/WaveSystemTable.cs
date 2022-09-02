using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
0. 기획자와 프로그래머의 대화가 1번.

1. 기획자의 테이블을 코드로 읽어올수 있도록 고민하는 것. => WaveSystemTable.
    정보 바구니인 구조체를 만들어서 기획용 테이블을 원하는대로 구현해주는 것

2. 웨이브마다 죽인 몬스터의 숫자를 기록하는 것 => _killCount
    GetCurrentWave()를 사용해서 웨이브 넘어가도 되는지 체크.

3. 웨이브와 콤보에 맞춰 점수를 제공하는 것 => GetCurrentScore()함수.
    _monsterKillScore에 점수를 쌓고, 최종적으로 랭킹 화면에도 보여줄 것.
 */


//> 정보 바구니라고생각하세요.
//> 내가 만든거니까 유니티가 어떻게 보여줘야 할지 모르겠어.
[System.Serializable] //> 이렇게 보여줘.
public struct WaveInformation
{
    public int waveID;             //> 웨이브 번호.
    public int maxEnemyCount;      //> 이번 웨이브에 등장하는 최대 몬스터 숫자.
    public int[] scoreLevel;       //> 점수를 획득하기 위한 검증해야 하는 최소 콤보 범위.
    public int[] scores;           //> 콤보에 맞춰 제공하는 점수.
    public GameObject[] enemy;  //> 웨이브에 나올 몬스터들.
    public GameObject[] flyEnemy;  //> 웨이브에 나올 몬스터들.
    public float speeds;         //> 등장하는 몬스터의 각 속도.
}

public class WaveSystemTable : MonoBehaviour
{
    public WaveInformation[] waves;
    public GameObject[] waveImage;
    public Transform[] pos;
    public Transform[] flyPos;

    private int currentWaveIndex;
    private bool changeWave;

    private void Start()
    {
        changeWave = false;
        currentWaveIndex = 0;
    }

    public void StartWave()
    {
        if (changeWave == false)
        {
            changeWave = true;
            StartCoroutine("Spawner", currentWaveIndex);
            waveImage[currentWaveIndex].SetActive(true);
            Invoke("WaveChage", 0.5f);
            Invoke("WaveChangeEnd", 1.2f);
        }
    }

    public void WaveUp()
    {
        if (waves.Length - 1 > currentWaveIndex && changeWave == false)
        {
            changeWave = true;
            currentWaveIndex++;
            StopCoroutine("Spawner");
            StartCoroutine("Spawner", currentWaveIndex);
            waveImage[currentWaveIndex].SetActive(true);
            Invoke("WaveChage", 0.5f);
            Invoke("WaveChangeEnd", 1.2f);
        }
    }

    public void WaveChage()
    {
        waveImage[currentWaveIndex].SetActive(false);
    }
    public void WaveChangeEnd()
    {
        changeWave = false;
    }

    //> ref는 원본을 넘겨주는 것.
    public int GetCurrentWave(int waveID, ref int killCount)
    {
        if (killCount == waves[waveID - 1].maxEnemyCount)
        {
            //> 다음 웨이브.
            killCount = 0;
            return ++waveID;
        }
        else
        {
            return waveID;
        }
    }

    //> public : 공용화되죠. 밖에서 볼수 있죠. (== 밖에서 접근 할 수 있다.)
    //> 반환종류 : 이 함수가 반환해주는 종류.
    //> 매개변수 : 함수안에서 사용하기 위해 같이 가지고 오는 변수.
    public int GetCurrentScore(int waveID, int combo)
    {
        //> waveID는 1부터 시작하는데, 배열은 0부터 시작한다. 그래서 1을 빼서 조정해주는것.
        int id = waveID - 1;

        //> 스코어 레벨은 콤보 숫자의 최소값이 적혀있기 때문에, combo와 매칭되는 것을 찾아줘야 한다.
        for (int i = 0; i < waves[id].scoreLevel.Length; i++)
        {
            if (combo < waves[id].scoreLevel[i])
            {
                //> 작은 범위를 체크하고 있기 때문에, 전단계의 score를 가져와야 하므로 (i - 1)을 사용한다.
                return waves[id].scores[i - 1];
            }
        }

        return 0;
    }

    private IEnumerator Spawner(int waveID)
    {
        yield return new WaitForSeconds(waves[waveID].speeds);
        int enemyIndex;
        int posIndex;

        if (waves[waveID].enemy.Length > 0)
        {
            enemyIndex = Random.Range(0, waves[waveID].enemy.Length);
            posIndex = Random.Range(0, pos.Length);

            Instantiate(waves[waveID].enemy[enemyIndex], pos[posIndex].position, Quaternion.identity);

        }
        if (waves[waveID].flyEnemy.Length > 0)
        {
            enemyIndex = Random.Range(0, waves[waveID].flyEnemy.Length);
            posIndex = Random.Range(0, flyPos.Length);

            Instantiate(waves[waveID].flyEnemy[enemyIndex], flyPos[posIndex].position, Quaternion.identity);
        }

        StartCoroutine("Spawner", waveID);
    }
}