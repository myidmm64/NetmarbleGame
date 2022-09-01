using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text = null;

    private void Update()
    {
        _text.SetText(Time.time.ToString("N2"));
    }
}
