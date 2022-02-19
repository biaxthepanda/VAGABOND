using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private GameObject _mainMenuCanvas;
    
    [SerializeField]
    private GameObject _inGameCanvas;
    
    void Awake()
    {
        if (_mainMenuCanvas == null)
            _mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        
        if (_inGameCanvas == null)
            _inGameCanvas = GameObject.Find("InGameCanvas");
        
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

    }

    public void DeactivateAll()
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
        Deactivated = 4,
    }
}
