using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
//using System.Drawing;

public class NewPlayerMove : MonoBehaviour
{
    public WaveSystemTable table;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public bool gameStart = false;

    private int _combo = 0;
    private int _killCount;
    private int _monsterKillScore;
    private int _currentWaveID = 1;

    public float moveSpeed = 3f;
    public float distance = 3f;
    private bool _isAttackable = true;

    private Coroutine _delayCo = null;
    [SerializeField]
    private UnityEvent<bool> OnFliped = null;
    private bool flip = false;

    [SerializeField]
    private List<GameObject> _targetList = null;
    public List<GameObject> TargetList { get => _targetList; set => _targetList = value; }
    public bool isStoped {get;set;}

    private Transform _visualTrm = null;

    private Animator _animator = null;

    [field: SerializeField]
    private UnityEvent<int> OnComboChange = null;
    [SerializeField]
    private TextMeshProUGUI _comboText = null;

    private void Awake()
    {
        _visualTrm = transform.Find("AgentVIsual");
        _animator = _visualTrm.GetComponent<Animator>();
        isStoped = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameStart && Time.timeScale > 0)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            Vector3 mousePoint = Camera.main.ScreenToViewportPoint(mousePos);
            //Debug.Log(mousePoint.ToString());

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (mousePoint.x < 0.5)
                {
                    //콤보가 여기서 실행됨
                    MoveAndAttack(Vector2.left);
                    /*Debug.Log("Left"); */
                }
                else
                {
                    MoveAndAttack(Vector2.right);
                    //Debug.Log("Right");
                }
            }
     
        }
        
        if (_combo == 0)
        {
            _comboText.color = Color.white;
        }
        else if (_combo == 5)
        {
            _comboText.color = new Color(0.1568628f,0.6039216f,1,1);
        }
        else if (_combo == 30)
        {
            _comboText.color = new Color(0.5176471f, 0.9843137f, 0.5176471f,1);
        }
        else if (_combo == 60)
        {
            _comboText.color = new Color(0.3215686f, 0.8941177f, 0.8627451f,1);
        }
        else if (_combo == 100)
        {
            _comboText.color = new Color(1, 0.9607843f, 0.4313726f,1);
        }
        else if (_combo == 500)
        {
            _comboText.color = new Color(0.9215686f,0,0,1);
        }
    }

    private void MoveAndAttack(Vector2 dir)
    {
        if (_isAttackable == false) return;

        Sequence seq = DOTween.Sequence();
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.Brandish);
        AudioSystem.auidoSystem.AudioClipPlayOneShot(AudioSystem.AudioName.jump);

        flip = dir.x < 0 ? true : false;
        OnFliped?.Invoke(flip);

        GameObject target = ReturnTarget();

        if(target != null)
        {
            //target.GetComponent<SpriteRenderer>().color = Color.black;
            target.GetComponent<BoxCollider2D>().enabled = false;
            Vector3 targetPos = target.transform.position;

            if(targetPos.y < transform.position.y)
                targetPos.y = transform.position.y;
            targetPos.x += flip ? 0.3f : -0.3f;

            /*seq.Append(_visualTrm.DOMove(targetPos, 0.08f).SetEase(Ease.OutQuad));
            seq.AppendCallback(() =>
            {
                _visualTrm.position = transform.position;
            });*/
            if (Mathf.Abs(targetPos.x - transform.position.x) >= distance)
            {
                _visualTrm.position = targetPos;
                _visualTrm.position -= new Vector3(dir.x * distance, 0, 0);
            }
            _animator.SetTrigger($"Attack{Random.Range(0, 3)}");
            CameraManager.instance.CameraShake(4f, 20f, 0.2f);
            _combo++;
            ComboTextSet();
            OnComboChange?.Invoke(_combo);

            _killCount++;
            Debug.Log("killcount : " + _killCount);
            _currentWaveID = table.GetCurrentWave(_currentWaveID, ref _killCount);
            Debug.Log("current wave : " + _currentWaveID);

            _monsterKillScore += table.GetCurrentScore(_currentWaveID, _combo);
            Debug.Log("score : " + _monsterKillScore);

            //> 점수를화면에 출력하는 것.
            scoreText.text = "score : " + _monsterKillScore;
            scoreText2.text = "score : " + _monsterKillScore;

            target.GetComponent<Enemy>().Die();
            _targetList.Remove(target);
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
