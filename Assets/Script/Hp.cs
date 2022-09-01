<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
=======
using TMPro;
>>>>>>> Stashed changes
using UnityEngine;


public class Hp : MonoBehaviour
{
    [SerializeField]

    private float maxHp = 100;
    public float curHp = 100;
    float imsi = 0.00001f;
    private SpriteRenderer spriteRenderer;
    public float DamagedTime;
<<<<<<< Updated upstream
=======
    public GameObject RespawnPanel;
    public GameObject PauseButton;
    public GameObject ScoreText;
>>>>>>> Stashed changes

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
<<<<<<< Updated upstream
            curHp -= 10;
=======
            curHp -= 1f;
        }
        hpbar.fillAmount = curHp / maxHp;
        if (curHp <= 0)
        {
            RespawnPanel.SetActive(true);
            PauseButton.SetActive(false);
            ScoreText.SetActive(false);
            //scoreText.gameObject.SetActive(true);
            //scoreText.text = "score : " + _monsterKillScore;
            Time.timeScale = 0f;
>>>>>>> Stashed changes
        }
        HandheHp();
    }
    private void HandheHp()
    {
        curHp -= imsi;
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
        gameObject.layer = 8;
    }
}
