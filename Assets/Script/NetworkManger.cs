using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManger : MonoBehaviour
{
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject Timer;


    public void OnClickPlay()
    {
        Time.timeScale = 1f;
        DisconnectPanel.SetActive(false);
        Timer.SetActive(true);
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);

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

    private void Start()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);

        }
    }
}