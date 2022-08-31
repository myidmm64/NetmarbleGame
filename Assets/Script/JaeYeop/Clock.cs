using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text = null;

    public GameObject Flyspawner1;
    public GameObject Flyspawner2;
    public GameObject Flyspawner3;
    public GameObject Flyspawner4;
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;


    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;
    public GameObject Level5;
    public bool spawned = false;

    private float time;

    private void Start()
    {
        Flyspawner1.SetActive(false);
        Flyspawner2.SetActive(false);
        Flyspawner3.SetActive(false);
        Flyspawner4.SetActive(false);
        spawner3.SetActive(false);
        spawner4.SetActive(false);

    }
    private void Update()
    {
        if (time >= 0.5f)
        {
            Level1.SetActive(true);
        }
        if (time >= 1.5f)
        {
            Level1.SetActive(false);

        }
        if (spawned == false && time >= 30f)
        {
            Level2.SetActive(true);

            spawned = true;
            Flyspawner1.SetActive(true);
            Flyspawner2.SetActive(true);
        }
        if (time >= 31f)
        {
            Level2.SetActive(false);

        }
        if (time >= 60f)
        {
            Flyspawner1.SetActive(false);
            Flyspawner2.SetActive(false);
            Flyspawner3.SetActive(true);
            Flyspawner4.SetActive(true);
            Level3.SetActive(true);
        }
        if (time >= 61f)
        {
            Level3.SetActive(false);

        }
        if (time >= 90f)
        {
            spawner1.SetActive(false);
            spawner2.SetActive(false);
            spawner3.SetActive(true);
            spawner4.SetActive(true);
            Level4.SetActive(true);
        }
        if (time >= 91f)
        {
            Level4.SetActive(false);

        }


        time += Time.deltaTime;
        _text.SetText(Time.time.ToString("N2"));
    }
}
