using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangedEnemyBehaviour : EnemyBehaviour
{
    [SerializeField]
    private List<Sprite> _enemyEmptyHandSprites = new List<Sprite>();

    [SerializeField]
    private GameObject _throwObject;

    public override void Initialize(Transform player, Vector3 offset, float comeDuration = 2f, float slowMotionDuration = 10f, float slowMotionStartDistance = 1f, float slowMotionEndDistance = 0f)
    {
        base.Initialize(player, offset, comeDuration, slowMotionDuration, slowMotionStartDistance + 1, slowMotionEndDistance + 1);
        
        DOVirtual.DelayedCall(comeDuration + 1f, () => ThrowShuriken());
    }

    private void ThrowShuriken()
    {
        if (_isDead)
            return;
            
        var dir = GetPlayerDir();
        
        // Instantiate shuriken towards the player
    
        var throwObj = Instantiate(_throwObject,new Vector3(transform.position.x,transform.position.y,0),Quaternion.identity).transform;
        throwObj.parent = transform;
        throwObj.up = dir * -1f;
        SoundController.Instance.PlaySFX(SoundController.SoundEffects.Shuriken);
        throwObj.DOMove(_player.position,25f).SetEase(Ease.OutSine);
        
        _spriteRenderer.sprite = _enemyEmptyHandSprites[_selectedSprite];
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Cut")) return;
        if (_isDead) return;
        
        ThrowShuriken();
            
        base.OnTriggerEnter2D(col);
        
    }
}
