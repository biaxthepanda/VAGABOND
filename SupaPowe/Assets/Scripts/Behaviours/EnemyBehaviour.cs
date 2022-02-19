using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _enemySprites = new List<Sprite>();
    protected int _selectedSprite = 0;
    [SerializeField]
    private List<Sprite> _enemyDeadSprites = new List<Sprite>();

    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    protected Transform _player;

    public virtual void Initialize(Transform player, float comeDuration = 2f, float slowMotionDuration = 10f, float slowMotionStartDistance = 7f, float slowMotionEndDistance = 6f)
    {
        _player = player;
        SelectSprite();

        Vector3 dir = GetPlayerDir();
        Vector3 slowStartPos = _player.position + dir * slowMotionStartDistance;

        transform.DOMove(slowStartPos, comeDuration).SetEase(Ease.Linear).OnComplete((() =>
        {
            Vector3 slowEndPos = _player.position + dir * slowMotionEndDistance;
            transform.DOMove(slowEndPos, slowMotionDuration).SetEase(Ease.Linear);
        }));
    }

    private void SelectSprite()
    {
        if (_enemySprites.Count == 0) return;
        _selectedSprite = Random.Range(0, _enemySprites.Count);
        _spriteRenderer.sprite = _enemySprites[_selectedSprite];
    }

    protected Vector3 GetPlayerDir()
    {
        return (transform.position - _player.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Cut")) return;

        transform.DOKill();
        
        Vector3 dir = GetPlayerDir();
        Vector3 movePos = transform.position + dir * 3f;
        
        transform.DOMove(movePos, 5f).SetEase(Ease.OutSine);
        
        // cutting animation
        // blood particles
        // cutting sprite
    }
}
