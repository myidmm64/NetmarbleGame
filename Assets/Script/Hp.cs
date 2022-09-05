using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;


public class Hp : MonoBehaviour
{
    [SerializeField]
    private Image hpbar;

    public TextMeshProUGUI scoreText;
    private int _monsterKillScore;

    private float maxHp = 3;
    public float curHp;
    private SpriteRenderer spriteRenderer;
    public float DamagedTime;
    public GameObject RespawnPanel;
    public GameObject PauseButton;
    public GameObject _comboText;
    public GameObject Timer;

    void Start()
    {
        
        hpbar.fillAmount = curHp / maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            curHp -= 1f;
        }
        hpbar.fillAmount = curHp / maxHp;
        if (curHp <= 0)
        {
            RespawnPanel.SetActive(true);
            PauseButton.SetActive(false);
            _comboText.SetActive(false);
            Timer.SetActive(false);
            //scoreText.gameObject.SetActive(true);
            //scoreText.text = "score : " + _monsterKillScore;
            Time.timeScale = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        OnDamaged();
    }
    void OnDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 0, 0, 0.4f);

        Invoke("OffDamaged", DamagedTime);
    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 3;
    }
}
