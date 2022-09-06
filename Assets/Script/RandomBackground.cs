using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class RandomBackground : MonoBehaviour
{
    private GameObject background_1;
    private SpriteRenderer spriteRendererBackground_1;
    private GameObject background_2;
    private SpriteRenderer spriteRendererBackground_2;
    public Sprite[] sprites;
    public float speed;

    void Start()
    {
        background_1 = transform.GetChild(0).gameObject;
        background_2 = transform.GetChild(1).gameObject;

        spriteRendererBackground_1 = background_1.GetComponent<SpriteRenderer>();
        spriteRendererBackground_2 = background_2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        background_1.transform.position -= Vector3.right * speed * Time.deltaTime;
        background_2.transform.position -= Vector3.right * speed * Time.deltaTime;

        if (background_1.transform.position.x <= -25)
        {
            background_1.transform.position = new Vector3(50, 0, 0);
            spriteRendererBackground_1.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        if (background_2.transform.position.x <= -25)
        {
            background_2.transform.position = new Vector3(50, 0, 0);
            spriteRendererBackground_2.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}
