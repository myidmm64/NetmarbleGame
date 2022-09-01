using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
0. ��ȹ�ڿ� ���α׷����� ��ȭ�� 1��.

1. ��ȹ���� ���̺��� �ڵ�� �о�ü� �ֵ��� ����ϴ� ��. => WaveSystemTable.
    ���� �ٱ����� ����ü�� ���� ��ȹ�� ���̺��� ���ϴ´�� �������ִ� ��

2. ���̺긶�� ���� ������ ���ڸ� ����ϴ� �� => _killCount
    GetCurrentWave()�� ����ؼ� ���̺� �Ѿ�� �Ǵ��� üũ.

3. ���̺�� �޺��� ���� ������ �����ϴ� �� => GetCurrentScore()�Լ�.
    _monsterKillScore�� ������ �װ�, ���������� ��ŷ ȭ�鿡�� ������ ��.
 */


//> ���� �ٱ��϶������ϼ���.
//> ���� ����Ŵϱ� ����Ƽ�� ��� ������� ���� �𸣰ھ�.
[System.Serializable] //> �̷��� ������.
public struct WaveInformation
{
    public int waveID;             //> ���̺� ��ȣ.
    public int maxEnemyCount;      //> �̹� ���̺꿡 �����ϴ� �ִ� ���� ����.
    public int[] scoreLevel;       //> ������ ȹ���ϱ� ���� �����ؾ� �ϴ� �ּ� �޺� ����.
    public int[] scores;           //> �޺��� ���� �����ϴ� ����.
    public GameObject[] monsters;  //> ���̺꿡 ���� ���͵�.
    public float[] speeds;         //> �����ϴ� ������ �� �ӵ�.
}

public class WaveSystemTable : MonoBehaviour
{
    public WaveInformation[] waves;

    //> ref�� ������ �Ѱ��ִ� ��.
    public int GetCurrentWave(int waveID, ref int killCount)
    {
        if (killCount == waves[waveID - 1].maxEnemyCount)
        {
            //> ���� ���̺�.
            killCount = 0;
            return ++waveID;
        }
        else
        {
            return waveID;
        }
    }

    //> public : ����ȭ����. �ۿ��� ���� ����. (== �ۿ��� ���� �� �� �ִ�.)
    //> ��ȯ���� : �� �Լ��� ��ȯ���ִ� ����.
    //> �Ű����� : �Լ��ȿ��� ����ϱ� ���� ���� ������ ���� ����.
    public int GetCurrentScore(int waveID, int combo)
    {
        //> waveID�� 1���� �����ϴµ�, �迭�� 0���� �����Ѵ�. �׷��� 1�� ���� �������ִ°�.
        int id = waveID - 1;

        //> ���ھ� ������ �޺� ������ �ּҰ��� �����ֱ� ������, combo�� ��Ī�Ǵ� ���� ã����� �Ѵ�.
        for (int i = 0; i < waves[id].scoreLevel.Length; i++)
        {
            if (combo < waves[id].scoreLevel[i])
            {
                //> ���� ������ üũ�ϰ� �ֱ� ������, ���ܰ��� score�� �����;� �ϹǷ� (i - 1)�� ����Ѵ�.
                return waves[id].scores[i - 1];
            }
        }

        return 0;
    }
}