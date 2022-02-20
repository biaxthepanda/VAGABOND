using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundController : MonoBehaviour
{
    private bool _isRunning = false;
    
    [SerializeField]
    private float _speed = 2f;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += ActivateRunning;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= ActivateRunning;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }

    private void ActivateRunning(GameManager.GameState state)
    {
        _isRunning = state == GameManager.GameState.Idle ? true : false;
    }
}
