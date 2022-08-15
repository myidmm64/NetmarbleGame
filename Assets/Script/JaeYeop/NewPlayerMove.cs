using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewPlayerMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    private bool _isAttackable = true;
    private int Hp = 1;

    private Coroutine _delayCo = null;
    [SerializeField]
    private UnityEvent<bool> OnFliped = null;
    private bool flip = false;

    [SerializeField]
    private List<GameObject> _targetList = null;
    public List<GameObject> TargetList { get => _targetList; set => _targetList = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            MoveAndAttack(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            MoveAndAttack(Vector2.right);
        }
    }

    private void MoveAndAttack(Vector2 dir)
    {
        if (_isAttackable == false) return;

        flip = dir.x < 0 ? true : false;
        OnFliped?.Invoke(flip);

        GameObject target = ReturnTarget();

        if(target != null)
        {
            target.GetComponent<SpriteRenderer>().color = Color.black;
            Vector3 targetPos = target.transform.position;
            if(targetPos.y < transform.position.y)
                targetPos.y = transform.position.y;
            transform.position = targetPos;
            _targetList.Remove(target);
            Destroy(target);
        }
        else
        {
            transform.Translate(dir * moveSpeed);
            _delayCo = StartCoroutine(AttackCoroutine());
        }
    }

    private GameObject ReturnTarget()
    {
        GameObject target = null;
        float min = 1000f;
        foreach (var a in _targetList)
        {
            if (Mathf.Abs(a.transform.position.x - transform.position.x) < min)
            {
                if(flip)
                {
                    if (a.transform.position.x - transform.position.x < 0)
                    {
                        min = Mathf.Abs(a.transform.position.x - transform.position.x);
                        target = a;
                    }
                }
                else
                {
                    if (a.transform.position.x - transform.position.x > 0)
                    {
                        min = Mathf.Abs(a.transform.position.x - transform.position.x);
                        target = a;
                    }
                }
            }
        }
        return target;
    }

    private IEnumerator AttackCoroutine()
    {
        _isAttackable = false;
        yield return new WaitForSeconds(0.4f);
        _isAttackable = true;
    }

    public void Targetting()
    {
        foreach(var a in _targetList)
        {
            a.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
