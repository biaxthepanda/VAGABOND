using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangedEnemyBehaviour : EnemyBehaviour
{
    [SerializeField]
    private List<Sprite> _enemyEmptyHandSprites = new List<Sprite>();

    public override void Initialize(Transform player, float comeDuration = 2f, float slowMotionDuration = 10f, float slowMotionStartDistance = 1f, float slowMotionEndDistance = 0f)
    {
        base.Initialize(player, comeDuration, slowMotionDuration, slowMotionStartDistance + 1f);
        
        DOVirtual.DelayedCall(comeDuration + 0.2f, () => ThrowShuriken());
    }

    private void ThrowShuriken()
    {
        // Instantiate shuriken towards the player
        var dir = GetPlayerDir();
        if (!_isDead) 
            _spriteRenderer.sprite = _enemyEmptyHandSprites[_selectedSprite];
    }
}
