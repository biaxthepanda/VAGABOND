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

    Material _firstMat;
    public Material shockWaveMat;

    SpriteRenderer pl;

    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

    }


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

        if(GameManager.Instance.State == GameManager.GameState.Attacking)
        {
            StartCoroutine(ChangeBackGrounds());
        }
    }



    IEnumerator ChangeBackGrounds()
    {
        /*_firstMat = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        foreach(Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().material = shockWaveMat;
        }
        
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().material = _firstMat;
        }
        */
        _firstMat = pl.material;
        pl.material = shockWaveMat;
        yield return new WaitForSeconds(0.5f);
        pl.material = _firstMat;
    }

}
