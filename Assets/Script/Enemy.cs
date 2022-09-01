using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Animator animator;
    //public GameObject[] items;
    //Rigidbody2D rigid;
    public Vector2 boxSize;
    public LayerMask whatIsLayer;
    private SpriteRenderer spriteRenderer;
    public GameObject target;
    public float percentage;
    public float speed;
    public float damage;
    private bool isMove;
    public int Hp = 1;
    private float curTime = 0;
    public float coolTime = 0.5f;

    private void Start()
    {
        target = GameObject.Find("Player");
        isMove = true;
        //animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rigid = GetComponent<Rigidbody2D>();
    }
    //public void DirectionEnemy(float target, float baseobj)
    //{
    //    if (target < baseobj)
    //        animator.SetFloat("Direction", -1);
    //    else
    //        animator.SetFloat("Direction", 1);
    //}
    void FixedUpdate()
    {
        Vector3 distance = transform.position - target.transform.position;
        distance.x = distance.x > 0 ? 1 : -1;
        distance.y = distance.y > 0 ? 1 : -1;
        distance.z = 0;

        if (distance.x > 0)
            transform.localScale = new Vector3(1, 1, 1) * 1f;
        else
            transform.localScale = new Vector3(-1, 1, 1) * 1f;

        if (isMove)
        {
            //animator.SetBool("isRun", true);
            distance.y = 0;
            transform.Translate(distance * speed * Time.deltaTime * -1);
        }
        else
            //animator.SetBool("isRun", false);

        if (Hp <= 0 && gameObject.layer != 10) // dead
        {
            gameObject.layer = 10;
            float rand = Random.Range(0, 1);
            Debug.Log(rand);
            if (rand <= percentage)
            {
                //int index = Random.Range(0, items.Length);
                //Instantiate(items[index], transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }

        curTime -= Time.deltaTime;
        //rigid.velocity = new Vector2(-1, rigid.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && curTime <= 0)
            isMove = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && curTime <= 0)
            isMove = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && curTime <= 0)
        {
            isMove = false;
            Debug.Log("attack");
            StartCoroutine("Attack");
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Hp = Hp - damage;
        StartCoroutine("colorChage");
    }

    IEnumerator colorChage()
    {
        spriteRenderer.color = new Color32(0, 0, 0, 127);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    IEnumerator Attack()
    {
        //animator.SetBool("isAttack", true);
        curTime = 9999;
        yield return new WaitForSeconds(0.25f);

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, boxSize, whatIsLayer);
        
        curTime = coolTime;
        //animator.SetBool("isAttack", false);
    }
}
