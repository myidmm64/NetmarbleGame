using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
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
    public bool attack = false;
    public bool isDie = false;

    private void Start()
    {
        target = GameObject.Find("Player");
        isMove = true;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (isDie == false)
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
            {
                //animator.SetBool("isRun", false);
            }

            curTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Zone") && curTime <= 0)
        {
            isMove = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Zone") && curTime <= 0)
        {
            isMove = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Zone") && curTime <= 0)
        {
            isMove = false;
            StartCoroutine("Attack");
            animator.SetTrigger("IsBoom");
            if (attack == true)
            {
                Debug.Log("attack");
                Destroy(this.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Hp = Hp - damage;
        StartCoroutine("colorChage");
    }

    IEnumerator Attack()
    {
        //animator.SetBool("isAttack", true);
        curTime = 9999;
        yield return new WaitForSeconds(0.25f);
        target.GetComponent<Hp>().TakeDamage(damage);

        //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, boxSize, whatIsLayer);
        //for (int i = 0; i < collider2Ds.Length; i++)
        //{
        //    if (collider2Ds[i].GetComponent<Hp>())
        //    {
        //        collider2Ds[i].GetComponent<Hp>().TakeDamage(damage);
        //        //Debug.Log(collider2Ds[i].name + " On Damaged");
        //    }
        //}
        curTime = coolTime;
        attack = true;
        //animator.SetBool("isAttack", false);
    }

    public void Die()
    {
        isDie = true;
        animator.SetTrigger("IsDead");
        Destroy(this.gameObject, 0.35f);
    }
}
