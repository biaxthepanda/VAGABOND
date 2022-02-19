using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelController : Singleton<TutorialLevelController>
{
    [Serializable]
    public class LevelData
    {
        public int levelID = -1;
        public TutorialType tutorialType = TutorialType.LeftHand;
        public Transform tutorialStartLocation;
        public Transform tutorialEndLocation;

        public enum TutorialType
        {
            LeftHand = 0,
            RightHand = 1,
        }
    }

    [SerializeField]
    private GameObject _leftClickPrefab;
    [SerializeField]
    private GameObject _rightClickPrefab;

    [SerializeField]
    private List<LevelData> _levelData;

    private int _currentLevel = 0;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += NewLevel;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= NewLevel;
    }

    private void NewLevel(GameManager.GameState state)
    {
        if (state != GameManager.GameState.Defending) return;
        
        // EnemyController isTutorial = true
        StopAllCoroutines();

        StartCoroutine(TutorialStart(_levelData[_currentLevel]));
    }

    IEnumerator TutorialStart(LevelData levelData)
    { 
        yield break;   
        
        
        
        
        
        
    }
}
