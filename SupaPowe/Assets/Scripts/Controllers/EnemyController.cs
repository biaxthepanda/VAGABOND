using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<EnemyBehaviour> _enemyBehaviours;
    
    private List<Vector3> _enemyOffsets = new List<Vector3>();

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private GameObject _cutPrefab;

    private int _enemyCount
    {
        get
        {
            if (_enemyBehaviours == null) return -1;
            
            return _enemyBehaviours.Count;
        }
    }

    private void Awake()
    {
        if (_enemyBehaviours == null || _enemyBehaviours.Count == 0)
            _enemyBehaviours = new List<EnemyBehaviour>(FindObjectsOfType<EnemyBehaviour>());
           
        for (int i = 0; i < _enemyCount; i++)
        {
            var enemy = _enemyBehaviours[i];
            var position = enemy.transform.position;
            
            _enemyOffsets.Add(position);
            
            position *= 10f;
            
            enemy.transform.position = position;
        }
    }

    public void Attack()
    {
        if (_player == null)
            _player = GameObject.FindWithTag("Player").transform;

        var levelBehaviour = LevelBehaviour.Instance;
        var comeDuration = levelBehaviour.EnemyComeDuration;
        var slowMotionDuration = levelBehaviour.SlowMotionDuration;
        var slowStartDistance = levelBehaviour.SlowMotionStartDistance;
        var slowEndDistance = levelBehaviour.SlowMotionEndDistance;
            
        for (int i = 0; i < _enemyCount; i++)
        {
            // if (enemy.TryGetComponent(out RangedEnemyBehaviour rangedEnemy))
            // {
            //     rangedEnemy.Initialize(_player);
            // }
            var enemy = _enemyBehaviours[i];

            enemy.Initialize(_player, _enemyOffsets[i], comeDuration, slowMotionDuration, slowStartDistance, slowEndDistance);
        }

        DOVirtual.DelayedCall(comeDuration + 0.1f, (() =>
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Attacking);
        }));

        var waitForAct = comeDuration + slowMotionDuration + 0.1f;

        StartCoroutine(CutEnemies(waitForAct));
    }


    IEnumerator CutEnemies(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.ChangeState(GameManager.GameState.Act);

        bool win = true;
        foreach (var enemy in _enemyBehaviours)
        {
            if (enemy.IsDead)
            {
                if (_cutPrefab != null)
                {
                    var cut = Instantiate(_cutPrefab, enemy.transform.position, Quaternion.identity);
                    var destroyCut = 0.6f;
                    Destroy(cut, destroyCut);
                    yield return new WaitForSeconds(destroyCut / 2f);
                }
                continue;
            }

            win = false;
                
        }
        
        GameManager.Instance.ChangeState(GameManager.GameState.NoBlackScreen);
        
        foreach (var enemy in _enemyBehaviours)
        {
            if (enemy.IsDead)
            {
                enemy.CuttingAnimation();
            }
                
        }

        yield return new WaitForSeconds(3f);
        if (!win)
        {
            var cut = Instantiate(_cutPrefab, _player.transform.position, Quaternion.identity);
            var destroyCut = 0.6f;
            Destroy(cut, destroyCut);
            yield return new WaitForSeconds(destroyCut/2f);
            
            foreach (var enemy in _enemyBehaviours)
            {
                if (!enemy.IsDead)
                {
                    enemy.EnemyOut();
                }
            }
            yield return new WaitForSeconds(destroyCut/2f);
            
            GameManager.Instance.ChangeState(GameManager.GameState.Lose);
        }
        else
        {
            //player walks thru the right and next level starts
            GameManager.Instance.ChangeState(GameManager.GameState.Win);
        }
    }
}
