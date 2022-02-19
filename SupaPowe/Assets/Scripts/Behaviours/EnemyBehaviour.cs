using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    protected List<Sprite> _enemySprites = new List<Sprite>();

    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    protected Transform _player;

    public virtual void Initialize(Transform player, float comeDuration = 2f, float slowMotionDuration = 10f, float minDistance = 1f)
    {
        _player = player;
        SelectSprite();

        Vector3 dir = (transform.position - _player.position).normalized;
        Vector3 movePos = _player.position + dir * minDistance;

        transform.DOMove(movePos, comeDuration).SetEase(Ease.InSine).OnComplete((() =>
        {
            movePos = _player.position + dir * 0.5f;
            transform.DOMove(movePos, slowMotionDuration).SetEase(Ease.Linear);
        }));
    }

    protected void SelectSprite()
    {
        if (_enemySprites.Count == 0) return;

        var randomSpriteIndex = Random.RandomRange(0, _enemySprites.Count);

        _spriteRenderer.sprite = _enemySprites[randomSpriteIndex];
    }
}
