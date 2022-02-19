using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<EnemyBehaviour> _enemyBehaviours;

    [SerializeField]
    private Transform _player;

    public void Attack()
    {
        if (_player == null)
            _player = GameObject.FindWithTag("Player").transform;

        var levelBehaviour = LevelBehaviour.Instance;
        var comeDuration = levelBehaviour.EnemyComeDuration;
        var slowMotionDuration = levelBehaviour.SlowMotionDistance;
        var minDistance = levelBehaviour.SlowMotionDuration;
            
        foreach (var enemy in _enemyBehaviours)
        {
            // if (enemy.TryGetComponent(out RangedEnemyBehaviour rangedEnemy))
            // {
            //     rangedEnemy.Initialize(_player);
            // }

            enemy.Initialize(_player, comeDuration, slowMotionDuration, minDistance);
        }

        DOVirtual.DelayedCall(comeDuration + 0.1f, (() =>
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Attacking);
        }));
    }
}
