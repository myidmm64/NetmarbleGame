using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Hp : MonoBehaviour
{
    [SerializeField]
    private Image hpbar;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI OverScore;
    private int _monsterKillScore;

    private float maxHp = 3;
    public float curHp;
    private float curTime;
    private SpriteRenderer spriteRenderer;
    public float DamagedTime;
    public GameObject RespawnPanel;
    public GameObject PauseButton;
    public GameObject _comboText;
    public GameObject Timer;
    public GameObject Over;
    public Animator animator;

    void Start()
    { 
        hpbar.fillAmount = curHp / maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.P))
        {
            curHp -= 1f;
        }
        hpbar.fillAmount = curHp / maxHp;
        
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        
        if (curHp <= 0)
        {
            animator.SetTrigger("IsDead");
            Invoke("GameOver", 1f);
        }
        else
        {
            animator.SetTrigger("IsHit");
            OnDamaged();
        }
    }
    void OnDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color32(255, 0, 0, 100);

        Invoke("OffDamaged", DamagedTime);
    }

    void OffDamaged()
    {
        spriteRenderer.color = Color.white;
        gameObject.layer = 3;
    }

    void GameOver()
    {
        RespawnPanel.SetActive(true);
        PauseButton.SetActive(false);
        _comboText.SetActive(false);
        Timer.SetActive(false);
        Over.SetActive(true);
        //scoreText.gameObject.SetActive(true);
        //scoreText.text = "score : " + _monsterKillScore;
        Time.timeScale = 0f;
    }
}
