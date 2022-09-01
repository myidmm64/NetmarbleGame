using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManger : MonoBehaviour
{
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
<<<<<<< Updated upstream
    
=======
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject Timer;
    public GameObject Hpbar;
    public GameObject Combo;
<<<<<<< Updated upstream
    public GameObject ScoreText;
=======
    public GameObject scoreText;
>>>>>>> Stashed changes


>>>>>>> Stashed changes

    public void OnClickPlay()
    {
        Time.timeScale = 1f;
        DisconnectPanel.SetActive(false);
<<<<<<< Updated upstream
=======
        Timer.SetActive(true);
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);
        Hpbar.SetActive(true);
        Combo.SetActive(true);
<<<<<<< Updated upstream
        ScoreText.SetActive(true);
=======
        scoreText.SetActive(true);
>>>>>>> Stashed changes
    }

    public void OnClickStop()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);

>>>>>>> Stashed changes
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
            DisconnectPanel.SetActive(true);
            RespawnPanel.SetActive(false);
        }
    }
}