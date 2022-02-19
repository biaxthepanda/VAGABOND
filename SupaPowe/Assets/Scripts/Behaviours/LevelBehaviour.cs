using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelBehaviour : StaticInstance<LevelBehaviour>
{
    
    [SerializeField]
    private float _runningDelay = 5f;
    
    [SerializeField]
    private EnemyController _enemyController;

    public void Start()
    {
        // start walk
        Debug.Log("Running");
        
        //wait for delay and attack
        if (_enemyController == null)
            _enemyController = GetComponent<EnemyController>();
        DOVirtual.DelayedCall(_runningDelay, () => _enemyController.Attack());
        
        
    }
}
