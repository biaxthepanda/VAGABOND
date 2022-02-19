using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Camera Settings")]

    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private float _lerpDuration = 3f;
    
    [SerializeField]
    private bool _isSnapping = false;
    
    [Header("Camera States")]
    [SerializeField]
    private Transform menuPosition;
    
    [SerializeField]
    private Transform gamePosition;
    
    [SerializeField]
    private Transform bossPosition;

    private void Start()
    {
        if (_mainCamera == null)
            _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public void SwitchCamera(CamPosition camPos, float moveDuration = -1f)
    {
        Vector3 target = Vector3.zero;

        var duration = moveDuration.Equals(-1f) ? 0 : _lerpDuration;

        if (camPos == CamPosition.MenuPosition)
            target = new Vector3(menuPosition.position.x, menuPosition.position.y, _mainCamera.transform.position.z);
        else if (camPos == CamPosition.GamePosition)
            target = new Vector3(gamePosition.position.x, gamePosition.position.y, _mainCamera.transform.position.z);
        else if (camPos == CamPosition.BossPosition)
            target = new Vector3(bossPosition.position.x, bossPosition.position.y, _mainCamera.transform.position.z);

        _mainCamera.transform.DOMove(target, duration, _isSnapping).SetEase(Ease.OutSine);
    }
    
    public enum CamPosition
    {
        MenuPosition,
        GamePosition,
        BossPosition,
    }
}
