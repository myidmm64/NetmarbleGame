using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private bool _isAttackable = true;
    private int Hp = 1;
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
    private void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_isAttackable == false) return;
            transform.Translate(Vector2.left * 3f);
            StartCoroutine(AttackCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (_isAttackable == false) return;
            transform.Translate(Vector2.right * 3f);
            StartCoroutine(AttackCoroutine());
        }



        /*Rigidbody2D rb;
        [SerializeField] float speed = 500f;

        Touch touch;
        Vector3 touchPos, moveDir;
        float previosTouch, currentTouch;

        bool isMoving = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (isMoving)
                currentTouch = (touchPos - transform.position).magnitude;

            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    previosTouch = 0;
                    currentTouch = 0;
                    isMoving = true;
                    touchPos = Camera.main.ScreenToViewportPoint(touch.position);
                    touchPos.z = 0;
                    moveDir = (touchPos - transform.position).normalized;
                    rb.velocity = new Vector2(moveDir.x * speed * Time.deltaTime, moveDir.y * speed * Time.deltaTime);
                }
            }

            if(currentTouch > previosTouch){
                isMoving = false;
                rb.velocity = Vector2.zero;
            }

            if (isMoving)
                previosTouch = (touchPos - transform.position).magnitude;
        }*/
    }
    private void FixedUpdate()
    {
        if (Hp <= 0) // dead
        {
            DisconnectPanel.SetActive(false);
            RespawnPanel.SetActive(true);
        }
    }
    private IEnumerator AttackCoroutine()
    {
        _isAttackable = false;
        yield return new WaitForSeconds(0.4f);
        _isAttackable = true;
    }
}