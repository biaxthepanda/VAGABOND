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
    
    void Start()
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
        else
        {
            Debug.Log("UI not affected.");
        }

    }

    private void DeactivateAll()
    {
        _mainMenuCanvas.SetActive(false);
        _inGameCanvas.SetActive(false);
    }

    [Serializable]
    public enum CurrentUI
    {
        MainMenu = 0,
        InGame = 1,
        Deactivated = 4,
    }
}
