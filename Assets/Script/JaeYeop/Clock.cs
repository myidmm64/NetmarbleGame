using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text = null;

    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject Level1;
    public GameObject Level2;   
    public bool spawned = false;

    private float time;

    private void Start()
    {
        spawner1.SetActive(false);
        spawner2.SetActive(false);
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
            spawner1.SetActive(true);
            spawner2.SetActive(true);
        }
        if (time >= 31f)
        {
            Level2.SetActive(false);

        }
        time += Time.deltaTime;
        _text.SetText(Time.time.ToString("N2"));
    }
}
