using System;
using UnityEngine;

public class RangedEnemyBehaviour : EnemyBehaviour
{

    public override void Initialize(Transform player, float comeDuration = 2f, float slowMotionDuration = 10f, float minDistance = 1f)
    {
        base.Initialize(player, comeDuration, slowMotionDuration, minDistance);
        
        ThrowShuriken();
    }

    private void ThrowShuriken()
    {
        throw new NotImplementedException();
    }
}
