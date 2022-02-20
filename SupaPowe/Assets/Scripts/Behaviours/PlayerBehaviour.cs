using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _bloodPrefab;
    
    [SerializeField]
    private Animator _animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (_bloodPrefab == null)
            _bloodPrefab = transform.GetChild(0).gameObject;

        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnStateChange;
    }

    private void OnStateChange(GameManager.GameState state)
    {
        switch (state)
        { 
            case GameManager.GameState.Menu: 
            case GameManager.GameState.Idle:
                WalkingAnimation();
                break;
            case GameManager.GameState.Attacking:
                AttackAnim();
                break;
            case GameManager.GameState.Win:
                AttackDoneAnim();
                break;
            case GameManager.GameState.Lose:
                DieAnim();
                break;
               
        }
    }


    private void AttackAnim()
    {
        _animator.Play("Attack");
        //play at the start of Attacking state
        //on animator connect attack and attackidle together
    }
    private void AttackDoneAnim()
    {
        _animator.Play("PuttingSwordBack");
        DOVirtual.DelayedCall(1f, () =>
        {
            _animator.Play("Walk");
            transform.DOMove(transform.right * 10f, 3f).SetEase(Ease.Linear).OnComplete((() =>
            {
                GameManager.Instance.ChangeState(GameManager.GameState.SceneChange);
            }));
        });
    }
    private void WalkingAnimation()
    {
        transform.DOKill();
        transform.position = Vector3.zero;
        _animator.Play("Walk");
        //play at the start of idle scene
    }

    private void DieAnim()
    {
        //play at the start of Lose state
        _bloodPrefab.SetActive(true);
        DOVirtual.DelayedCall(3f, () => _bloodPrefab.SetActive(false));
        
        DOVirtual.DelayedCall(5f, () => GameManager.Instance.ChangeState(GameManager.GameState.SceneChange));
    }
}
