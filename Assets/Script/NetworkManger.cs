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
<<<<<<< HEAD
    public GameObject Hpbar;
    public GameObject Combo;
<<<<<<< HEAD
<<<<<<< Updated upstream
    public GameObject ScoreText;
=======
    public GameObject scoreText;
>>>>>>> Stashed changes
=======
>>>>>>> Lch

=======
>>>>>>> parent of 18e07ba (lch)


    public void OnClickPlay()
    {
        Time.timeScale = 1f;
        DisconnectPanel.SetActive(false);
        Timer.SetActive(true);
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);
<<<<<<< HEAD
        Hpbar.SetActive(true);
        Combo.SetActive(true);
<<<<<<< HEAD
<<<<<<< Updated upstream
        ScoreText.SetActive(true);
=======
        scoreText.SetActive(true);
>>>>>>> Stashed changes
=======
>>>>>>> Lch
=======

>>>>>>> parent of 18e07ba (lch)
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

<<<<<<< HEAD
    public void OnClickRestart()
    {
        //ù ����� �������� �ȴ�.
        //GetActiveScene.name�� ���� ���� scene�� �̸��� �޾ƿ´�.
        //LoadScene�� ���� �ش� scene�� �����Ѵ�.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

=======
>>>>>>> parent of 18e07ba (lch)
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
<<<<<<< HEAD
        
        /*if (RespawnPanel == true)
        {
            PauseButton.SetActive(false);
        }*/
=======
>>>>>>> parent of 18e07ba (lch)
    }
}