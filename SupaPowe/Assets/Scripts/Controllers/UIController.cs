using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private GameObject _mainMenuCanvas;
    
    [SerializeField]
    private GameObject _inGameCanvas;
    
    [SerializeField]
    private Image _blackScreenCanvasImage;

    [SerializeField]
    Sprite _deathImage;
    
    void Awake()
    {
        if (_mainMenuCanvas == null)
            _mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        
        if (_inGameCanvas == null)
            _inGameCanvas = GameObject.Find("InGameCanvas");
        
        if (_blackScreenCanvasImage == null)
            _blackScreenCanvasImage = GameObject.Find("BlackScreenCanvas").GetComponentInChildren<Image>();
        
        _blackScreenCanvasImage.DOFade(0, 1);
        
    }

    public void ChangeCanvasView(CurrentUI ui)
    {
        DeactivateAll();

        if (ui == CurrentUI.MainMenu)
        {
            _mainMenuCanvas.SetActive(true);
        }
        else if (ui == CurrentUI.InGame)
        {
            _inGameCanvas.SetActive(true);
        }
        else if (ui == CurrentUI.FadeInFadeOut)
        {
            
            _blackScreenCanvasImage.DOFade(0, 0);
            
            _blackScreenCanvasImage.DOFade(1, 1).OnComplete((() =>
            {
                GameManager.Instance.ChangeState(GameManager.GameState.Idle);
                _blackScreenCanvasImage.DOFade(0, 1);
            }));
        }
        else if (ui == CurrentUI.BlackScreen)
        {
            _blackScreenCanvasImage.DOFade(0.5f, 0.5f);
        }
        else if (ui == CurrentUI.NoBlackScreen)
        {
            _blackScreenCanvasImage.DOFade(0, 0.5f);
        }

    }

    private void DeactivateAll()
    {
        _mainMenuCanvas.SetActive(false);
        _inGameCanvas.SetActive(false);
    }

    public void Play()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Idle);
    }
    public void Exit()
    {
        Application.Quit();
    }

    [Serializable]
    public enum CurrentUI
    {
        MainMenu = 0,
        InGame = 1,
        Attacking = 2,
        BlackScreen = 3,
        NoBlackScreen = 4,
        Deactivated = 5,
        FadeInFadeOut = 6,
    }

    public void BlackScreenToDeath()
    {
        _blackScreenCanvasImage.sprite = _deathImage;
        DOVirtual.DelayedCall(0.5f, ()=> _blackScreenCanvasImage.sprite = null);
    }
}
