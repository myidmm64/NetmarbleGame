using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class NewPlayerMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    private bool _isAttackable = true;

    private Coroutine _delayCo = null;
    [SerializeField]
    private UnityEvent<bool> OnFliped = null;
    private bool flip = false;

    [SerializeField]
    private List<GameObject> _targetList = null;
    public List<GameObject> TargetList { get => _targetList; set => _targetList = value; }

    private Transform _visualTrm = null;

    private Animator _animator = null;

    public WaveSystemTable table;
    public TextMeshProUGUI scoreText;

    private int _combo = 0;
    private int _monsterKillScore;

    [field: SerializeField]
    private UnityEvent<int> OnComboChange = null;
    [SerializeField]
    private TextMeshProUGUI _comboText = null;

    private void Awake()
    {
        _visualTrm = transform.Find("AgentVIsual");
        _animator = _visualTrm.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            Vector3 mousePoint = Camera.main.ScreenToViewportPoint(mousePos);

            //Debug.Log(mousePoint.ToString());

            if (mousePoint.x < 0.5) { MoveAndAttack(Vector2.left); /*Debug.Log("Left"); */}
            else MoveAndAttack(Vector2.right);  //Debug.Log("Right");
        }
        //_monsterKillScore += WaveSystemTable.currentMonsterScore;
        /*if (Input.GetMouseButtonDown(1))
        {
            MoveAndAttack(Vector2.left);
        }
        if (Input.GetMouseButtonDown(1))
        {
            MoveAndAttack(Vector2.right);
        }*/
    }

    private void MoveAndAttack(Vector2 dir)
    {
        if (_isAttackable == false) return;

        Sequence seq = DOTween.Sequence();

        flip = dir.x < 0 ? true : false;
        OnFliped?.Invoke(flip);

        GameObject target = ReturnTarget();

        if(target != null)
        {
            target.GetComponent<SpriteRenderer>().color = Color.black;
            Vector3 targetPos = target.transform.position;

            if(targetPos.y < transform.position.y)
                targetPos.y = transform.position.y;
            targetPos.x += flip ? 0.3f : -0.3f;

            /*seq.Append(_visualTrm.DOMove(targetPos, 0.08f).SetEase(Ease.OutQuad));
            seq.AppendCallback(() =>
            {
                _visualTrm.position = transform.position;
            });*/
            _visualTrm.position = targetPos;
            _animator.SetTrigger($"Attack{Random.Range(0, 3)}");
            CameraManager.instance.CameraShake(4f, 20f, 0.2f);
            _combo++;
            ComboTextSet();
            OnComboChange?.Invoke(_combo);

<<<<<<< HEAD
            _killCount++;
            Debug.Log("killcount : " + _killCount);
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
            _currentWaveID = table.GetCurrentWave(_currentWaveID, ref _killCount);
            Debug.Log("current wave : " + _currentWaveID);

            _monsterKillScore += table.GetCurrentScore(_currentWaveID, _combo);
            Debug.Log("score : " + _monsterKillScore);

            scoreText.text = "score : " + _monsterKillScore;

=======
>>>>>>> Lch
            _targetList.Remove(target);
            Destroy(target);
        }
        else
        {
            /*seq.Append(_visualTrm.DOMove(transform.position + (Vector3)dir * moveSpeed, 0.08f).SetEase(Ease.OutQuad));
            seq.AppendCallback(() =>
            {
                _visualTrm.position = transform.position;
            });*/
            _visualTrm.position = transform.position + (Vector3)dir * moveSpeed;
            _animator.SetTrigger($"Attack{Random.Range(0, 3)}");
            CameraManager.instance.CameraShake(4f, 20f, 0.2f);
            _combo = 0;
            ComboTextSet();
            OnComboChange?.Invoke(_combo);

            _delayCo = StartCoroutine(AttackCoroutine());
        }
    }

    private void ComboTextSet()
    {
        _comboText.transform.DOKill();
        _comboText.SetText($"{_combo} combo !");
        _comboText.transform.localScale = Vector3.one * 1.5f;
        _comboText.transform.DOScale(Vector3.one, 0.2f);
    }

    public void ReturnOrigin()
    {
        _visualTrm.position = transform.position;
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

    /*public void Targetting()
    {
        foreach(var a in _targetList)
        {
            a.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }*/
}
