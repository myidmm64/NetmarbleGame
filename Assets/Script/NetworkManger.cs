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
    public WaveSystemTable wave;
    private float curTime;
    private TextMeshProUGUI timeText;

    public void OnClickPlay()
    {
        Time.timeScale = 1f;
        DisconnectPanel.SetActive(false);
        Timer.SetActive(true);
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);
        Hpbar.SetActive(true);
        Combo.SetActive(true);
    }

    public void OnClickStop()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);

    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickRestart()
    {
        //첫 장면을 가져오게 된다.
        //GetActiveScene.name를 통해 현재 scene의 이름을 받아온다.
        //LoadScene을 통해 해당 scene을 실행한다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private void Start()
    {
        Time.timeScale = 0f;
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

        curTime += Time.deltaTime;

        if (timeText)
            timeText.text = curTime.ToString("F2");

        switch (timeText.text)
        {
            case "1.00":
                wave.StartWave();
                break;
            case "30.00":
                wave.WaveUp();
                break;
            case "60.00":
                wave.WaveUp();
                break;
            case "90.00":
                wave.WaveUp();
                break;
        }

        /*if (RespawnPanel == true)
        {
            PauseButton.SetActive(false);
        }*/
    }
}