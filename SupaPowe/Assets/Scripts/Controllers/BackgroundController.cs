using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundController : MonoBehaviour
{


    public bool isRunning;
    public float speed;

    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime);
        }
    }

    private void ActivateRunning()
    {
        isRunning = true;
    }
}
