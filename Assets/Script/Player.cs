using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(X, Y)*Time.deltaTime* moveSpeed); 
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