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
    private float _slowMotionStartDistance = 7f;
    public float SlowMotionStartDistance => _slowMotionStartDistance;
    
    [SerializeField]
    private float _slowMotionEndDistance = 7f;
    public float SlowMotionEndDistance => _slowMotionEndDistance;

    [SerializeField]
    private float maxDrawDrawDistance = 5f;
    public float MaxDrawDistance => maxDrawDrawDistance;

    [SerializeField]
    private int _maxDrawCount = 5;
    public int MaxDrawCount => _maxDrawCount;

    [SerializeField]
    private EnemyController _enemyController;

    public void Start()
    {
        // start walk
        
        //wait for delay and attack
        if (_enemyController == null)
            _enemyController = GetComponentInChildren<EnemyController>();

        _slowMotionDuration *= LevelManager.Instance.SlowTimeMultiplier;
        
        DOVirtual.DelayedCall(_runningDelay, () =>
        {
            SoundController.Instance.PlayMusic(SoundController.Musics.Combat);
            _enemyController.Attack();
            GameManager.Instance.ChangeState(GameManager.GameState.Defending);
        });

    }
}
