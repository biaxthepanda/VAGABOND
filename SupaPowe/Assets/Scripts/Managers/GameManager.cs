using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged;

    public GameState State { get; private set; }

    [SerializeField]
    private UIController _uiController;
    [SerializeField]
    private CameraController _cameraController;

    private bool _isStarted = false;
    
    void Start()
    {
        if (_uiController == null)
            _uiController = GameObject.Find("UIController").GetComponent<UIController>();
        
        if (_cameraController == null)
            _cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();
        
        
        ChangeState(GameState.Menu);
    }
    
    public void ChangeState(GameState newState)
    {
        State = newState;
        
        switch (newState)
        {
            case GameState.Menu:
                Menu();
                break;
            case GameState.Idle:
                Idle();
                break;
            case GameState.Defending:
                break;
            case GameState.Attacking:
                break;
            case GameState.Act:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
                
        }
        
        OnGameStateChanged?.Invoke(newState);
        
        Debug.Log($"New State: {newState}");
    }

    private void Menu()
    {
        _uiController.ChangeCanvasView(UIController.CurrentUI.MainMenu);
        SoundController.Instance.PlayMusic(SoundController.Musics.Menu);
        var moveDuration = _isStarted ? -1 : 0;
        if(!_isStarted)
            _isStarted = true;
        _cameraController.SwitchCamera(CameraController.CamPosition.MenuPosition, 0f);
    }
    private void Idle()
    {
        _uiController.DeactivateAll();
        SoundController.Instance.PlayMusic(SoundController.Musics.GameLoop);
        _cameraController.SwitchCamera(CameraController.CamPosition.GamePosition);
        
    }
    private void Defending()
    {
        //EnemyController.Initialize();
    }
    private void Attacking()
    {
        _uiController.ChangeCanvasView(UIController.CurrentUI.InGame);
    }
    private void Act()
    {
        
    }
    private void Win()
    {
        
    }
    private void Lose()
    {
        
    }


    [Serializable]
    public enum GameState
    {
        Menu = 0,
        Idle = 1, // Idle, then Run
        Defending = 2, // Enemies starts to attack
        Attacking = 3, // Where we'll take inputs
        Act = 4, // Cutting animations etc.
        Win = 5,
        Lose = 6,
    }
}
