using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private NewPlayerMove _playerMove = null;
    [SerializeField]
    private UnityEvent OnTargetPlus = null;
    [SerializeField]
    private UnityEvent OnTargetMinas = null;

    private void Awake()
    {
        //_playerMove = transform.root.GetComponent<NewPlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _playerMove.TargetList.Add(collision.gameObject);
            OnTargetPlus?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _playerMove.TargetList.Remove(collision.gameObject);
            OnTargetMinas?.Invoke();
        }
    }
}
