using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using TMPro;
=======
>>>>>>> parent of 75bd87e (lch)
using UnityEngine;


public class Hp : MonoBehaviour
{
    [SerializeField]

<<<<<<< HEAD
    public TextMeshProUGUI scoreText;
    private int _monsterKillScore;

    private float maxHp = 3;
    public float curHp;
=======
    private float maxHp = 100;
    public float curHp = 100;
    float imsi = 0.00001f;
>>>>>>> parent of 18e07ba (lch)
    private SpriteRenderer spriteRenderer;
    public float DamagedTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
<<<<<<< HEAD
<<<<<<< HEAD
            curHp -= 1f;
        }
        hpbar.fillAmount = curHp / maxHp;
        if (curHp <= 0)
        {
            RespawnPanel.SetActive(true);
            PauseButton.SetActive(false);
            //scoreText.gameObject.SetActive(true);
            //scoreText.text = "score : " + _monsterKillScore;
            Time.timeScale = 0f;
=======
            curHp -= 10;
>>>>>>> parent of 18e07ba (lch)
=======
            curHp -= 10;
>>>>>>> parent of 75bd87e (lch)
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
