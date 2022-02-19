using System.Collections;
using System.Collections.Generic;
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
            
            
        foreach (var enemy in _enemyBehaviours)
        {
            // if (enemy.TryGetComponent(out RangedEnemyBehaviour rangedEnemy))
            // {
            //     rangedEnemy.Initialize(_player);
            // }

            enemy.Initialize(_player);
        }
    }
}
