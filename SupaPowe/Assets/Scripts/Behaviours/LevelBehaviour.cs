using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelBehaviour : StaticInstance<LevelBehaviour>
{
    
    [SerializeField]
    private float _runningDelay = 5f;
    
    [SerializeField]
    private float _enemyComeDuration = 2f;
    public float EnemyComeDuration => _enemyComeDuration;
    
    [SerializeField]
    private float _slowMotionDuration = 10f;
    public float SlowMotionDuration => _slowMotionDuration;
    
    [SerializeField]
    private float _slowMotionDistance = 7f;
    public float SlowMotionDistance => _slowMotionDistance;

    [SerializeField]
    private float _maxDistance = 5f;
    public float MaxDistance => _maxDistance;

    [SerializeField]
    private int _maxDrawing = 5;
    public int MaxDrawing => _maxDrawing;

    [SerializeField]
    private EnemyController _enemyController;

    public void Start()
    {
        // start walk
        
        //wait for delay and attack
        if (_enemyController == null)
            _enemyController = GetComponentInChildren<EnemyController>();
        DOVirtual.DelayedCall(_runningDelay, () =>
        {
            _enemyController.Attack();
            GameManager.Instance.ChangeState(GameManager.GameState.Defending);
        });
        
    }
}
