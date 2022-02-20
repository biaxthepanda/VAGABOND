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
    public float SlowTimeMultiplier { get; set; }= 1f;
    public float LineLengthMultiplier { get; set; } = 1f;
    public int BonusLine { get; set; } = 0;

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
            if (CurrentLevel >= _levels.Count) return;
            
            DOVirtual.DelayedCall(1f, () => _activeLevel = Instantiate(_levels[CurrentLevel], _levelParent));

            return;
        } 
        else if (state == GameManager.GameState.Win)
        {
            DOVirtual.DelayedCall(3, () =>
            {
                Destroy(_activeLevel);
                _activeLevel = null;

                _currentLevel++;
            });
        } 
        else if (state == GameManager.GameState.Lose)
        {
            DOVirtual.DelayedCall(3, () =>
            {
                Destroy(_activeLevel);
                _activeLevel = null;
            });
        }
        
        
    }
}
