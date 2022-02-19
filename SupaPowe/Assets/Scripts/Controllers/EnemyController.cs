using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<EnemyBehaviour> _enemyBehaviours;

    public void Attack()
    {
        foreach (var enemy in _enemyBehaviours)
        {
            if (enemy.TryGetComponent(out RangedEnemyBehaviour behaviour))
            {
                
            }
            else
            {
                
            }
        }
    }
}
