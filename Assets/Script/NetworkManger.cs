using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NetworkManger : MonoBehaviour
{
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject Timer;
    public GameObject Hpbar;
    public GameObject Combo;
    public GameObject Clock;
    public WaveSystemTable wave;
    private float curTime;
    private TextMeshProUGUI timeText;
    public NewPlayerMove playerMove;
    public Animator animator;

    public void Play()
    {
        playerMove.gameStart = true;
        DisconnectPanel.SetActive(false);
        Timer.SetActive(true);
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);
        Hpbar.SetActive(true);
        Combo.SetActive(true);
        Clock.SetActive(true);
    }
    public void OnClickPlay()
    {
        animator.SetTrigger("IsStart");
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.UI);
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.background);
        Invoke("Play", 1f);
    }


    public void OnClickStop()
    {
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.UI);
        Time.timeScale = 0f;
        PausePanel.SetActive(true);

    }

    public void OnClickQuit()
    {
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.UI);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickRestart()
    {
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.UI);
        //첫 장면을 가져오게 된다.
        //GetActiveScene.name를 통해 현재 scene의 이름을 받아온다.
        //LoadScene을 통해 해당 scene을 실행한다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private void Start()
    {
        Time.timeScale = 1f;
        curTime = 0f;
        timeText = Timer.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);

        }

        if (DisconnectPanel.activeSelf == false)
            curTime += Time.deltaTime;

        if (timeText)
            timeText.text = curTime.ToString("F2");

        switch ((int)curTime)
        {
            case 1:
                wave.StartWave();
                break;
            case 30:
                wave.WaveUp();
                break;
            case 60:
                wave.WaveUp();
                break;
            case 90:
                wave.WaveUp();
                break;
            case 120:
                wave.WaveUp();
                break;
        }

        /*if (RespawnPanel == true)
        {
            PauseButton.SetActive(false);
        }*/
    }
}