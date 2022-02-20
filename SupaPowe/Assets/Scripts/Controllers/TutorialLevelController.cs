using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DG.Tweening;
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

    private bool _isTutorialPlaying = false;
    
    private bool _isLeftClickDone = false;
    private bool _isRightClickDone = false;
    
    private GameObject _tutorialMouse = null;

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
        if (state != GameManager.GameState.Attacking) return;
        if (!IsTutorialLevel(LevelManager.Instance.CurrentLevel)) return;
        
        // EnemyController isTutorial = true
            
        StopAllCoroutines();
        
        if (_tutorialMouse != null)
        {
            _tutorialMouse.transform.DOKill();
            Destroy(_tutorialMouse);
            _tutorialMouse = null;
        }

        StartCoroutine(TutorialStart(_levelData[LevelManager.Instance.CurrentLevel]));
    }

    private void Update()
    {
        if (!_isTutorialPlaying) return;

        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonUp(1);

        if (leftClick || rightClick)
        {
            _isTutorialPlaying = false;
        }

        if (rightClick)
            _isRightClickDone = true;
        if (leftClick)
            _isLeftClickDone = true;

    }

    private bool IsTutorialLevel(int level)
    {
        foreach (var levelData in _levelData)
        {
            if (levelData.levelID == level)
                return true;
        }

        return false;
    }

    IEnumerator TutorialStart(LevelData levelData)
    {
        if (_isTutorialPlaying) yield break;
        
        
        var startPos = levelData.tutorialStartLocation.position;
        var endPos = levelData.tutorialEndLocation.position;
        
        _isTutorialPlaying = true;
        _isLeftClickDone = false;
        _isRightClickDone = false;

        bool isLevelLeftHand = levelData.tutorialType == LevelData.TutorialType.LeftHand;
        
        
        for (int i = 0; i < 3; i++)
        {        
            if (isLevelLeftHand)
            {
                if (_isLeftClickDone) break;
                _tutorialMouse = Instantiate(_leftClickPrefab, startPos, Quaternion.identity);

            } else
            {
                if (_isRightClickDone) break;
                _tutorialMouse = Instantiate(_rightClickPrefab, startPos, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(0.3f);
            
            if (_tutorialMouse != null)
            {
                _tutorialMouse.transform.DOMove(endPos, 2f);

                Destroy(_tutorialMouse, 2.1f);
            }

            yield return new WaitForSeconds(.5f);
            
            if (isLevelLeftHand)
                if (_isLeftClickDone) break;
                else
                if (_isRightClickDone) break;
            
            yield return new WaitForSeconds(.5f);
            
            if (isLevelLeftHand)
                if (_isLeftClickDone) break;
                else
                if (_isRightClickDone) break;
            
            yield return new WaitForSeconds(1f);
        }
        
        
        if (_tutorialMouse != null)
        {
            _tutorialMouse.transform.DOKill();
            Destroy(_tutorialMouse);
            _tutorialMouse = null;
        }
        

        _isTutorialPlaying = false;
    }
}
