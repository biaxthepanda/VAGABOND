using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelBehaviour : StaticInstance<LevelBehaviour>
{
    
    [SerializeField]
    private float _runningDelay = 5f;
    
    [SerializeField]
    private float _enemyComeDuration = 2f;
    public float EnemyComeDuration => _enemyComeDuration;
    
    [SerializeField]
    private float _slowMotionDuration = 10f;
    public float SlowMotionDuration => _slowMotionDuration;
    
    [SerializeField]
    private float _slowMotionStartDistance = 7f;
    public float SlowMotionStartDistance => _slowMotionStartDistance;
    
    [SerializeField]
    private float _slowMotionEndDistance = 7f;
    public float SlowMotionEndDistance => _slowMotionEndDistance;

    [SerializeField]
    private float maxDrawDrawDistance = 5f;
    public float MaxDrawDistance => maxDrawDrawDistance;

    [SerializeField]
    private int _maxDrawCount = 5;
    public int MaxDrawCount => _maxDrawCount;

    [SerializeField]
    private EnemyController _enemyController;
    
    [SerializeField]
    private int isBoss = -1;

    public void Start()
    {
        // start walk
        
        //wait for delay and attack
        if (_enemyController == null)
            _enemyController = GetComponentInChildren<EnemyController>();

        _slowMotionDuration *= LevelManager.Instance.SlowTimeMultiplier;

        DOVirtual.DelayedCall(1f, () =>
        {
            if (isBoss >= 0)
            {
                if (isBoss == 0)
                    DialogueCanvas.Instance.Dialogue("Slash at same direction.\nDefend at different direction.", 4f);
                else if (isBoss == 1)
                    DialogueCanvas.Instance.Dialogue("This samurai might be tricky, he's concealing his attacks.", 4f);
                SoundController.Instance.PlayDialogue(isBoss);
            }
        });

        
        DOVirtual.DelayedCall(_runningDelay, () =>
        {
            if (isBoss != -1)
                SoundController.Instance.PlayMusic(SoundController.Musics.Combat);
            _enemyController.Attack();
            GameManager.Instance.ChangeState(GameManager.GameState.Defending);

        });

    }
}
