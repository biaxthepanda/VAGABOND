using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    protected List<Sprite> _enemySprites = new List<Sprite>();

    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    protected Transform _player;

    public virtual void Initialize(Transform player, float comeDuration = 2f, float slowMotionDuration = 10f, float slowMotionStartDistance = 7f)
    {
        _player = player;
        SelectSprite();

        Vector3 dir = (transform.position - _player.position +  Random.insideUnitSphere).normalized;
        Vector3 movePos = _player.position + dir * slowMotionStartDistance;

        transform.DOMove(movePos, comeDuration).SetEase(Ease.Linear).OnComplete((() =>
        {
            movePos = _player.position + dir * 0.5f;
            transform.DOMove(movePos, slowMotionDuration).SetEase(Ease.Linear);
        }));
    }

    protected void SelectSprite()
    {
        if (_enemySprites.Count == 0) return;

        _spriteRenderer.sprite = _enemySprites.Rand();
    }
}
