using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private SpriteRenderer _spriteRederer = null;
    [SerializeField]
    private LayerMask _layerMask = 0;

    private void Awake()
    {
        _spriteRederer = GetComponent<SpriteRenderer>();
    }

    public void Flip(bool val)
    {
        if (val)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    /*private void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, transform.right, 2f, _layerMask);
        if (hit.collider != null)
        {
            Debug.Log($"{hit.collider.name}");
            hit.collider.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }*/
}
