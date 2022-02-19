using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged;

    public GameState State { get; private set; }
    
    void Start()
    {
        ChangeState(GameState.Menu);
    }
    
    public void ChangeState(GameState newState)
    {
        State = newState;
        
        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Idle:
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
        
    }
    private void Idle()
    {
        
    }
    private void Defending()
    {
        
    }
    private void Attacking()
    {
        
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
