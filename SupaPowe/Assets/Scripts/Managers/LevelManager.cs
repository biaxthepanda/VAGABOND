using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private List<GameObject> _levels;
    
    [SerializeField]
    private Transform _levelParent;

    private int _currentLevel = 0;
    private GameObject _activeLevel;

    public int CurrentLevel => _currentLevel;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += StartLevel;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= StartLevel;
    }

    private void StartLevel(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Idle)
        {
            _activeLevel = Instantiate(_levels[CurrentLevel], _levelParent);

            return;
        } 
        else if (state == GameManager.GameState.Win)
        {
            DOVirtual.DelayedCall(3, () =>
            {
                Destroy(_activeLevel);
                _activeLevel = null;

                _currentLevel++;
                
                GameManager.Instance.ChangeState(GameManager.GameState.Idle);
            });
        } 
        else if (state == GameManager.GameState.Lose)
        {
            DOVirtual.DelayedCall(3, () =>
            {
                Destroy(_activeLevel);
                _activeLevel = null;
                
                GameManager.Instance.ChangeState(GameManager.GameState.Idle);
            });
        }
        
        
    }
}