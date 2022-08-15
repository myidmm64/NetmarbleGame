using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private bool _isFlowBackground = false;
    private SpriteRenderer _spriteRenderer = null;

    private void Awake()
    {
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isFlowBackground)
        {
            float offset = Time.time * _speed;
            _spriteRenderer.material.mainTextureOffset = (offset * Vector2.right);
        }
    }

    public void Move(float amount)
    {
        if (amount > 0)
        {
            _spriteRenderer.material.mainTextureOffset += Vector2.right * _speed * Time.deltaTime;
        }
        if (amount < 0)
        {
            _spriteRenderer.material.mainTextureOffset += Vector2.right * _speed * -1f * Time.deltaTime;
        }
    }
}
