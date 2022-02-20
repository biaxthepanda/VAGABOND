using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InGameCanvasElement : MonoBehaviour
{

    [SerializeField]
    private Transform _drawChanceParent;
    
    [SerializeField]
    private GameObject _drawChancePrefab;

    [SerializeField]
    private Slider _timeSlowSlider;


    private void OnEnable()
    {
        
        if (!GameManager.Instance.IsStarted) return;
        
        DOVirtual.DelayedCall(0.15f, () =>
        {
            _timeSlowSlider.maxValue = 1f;
            _timeSlowSlider.minValue = 0f;
            _timeSlowSlider.value = 1f;
            
            var drawChance = LevelBehaviour.Instance.MaxDrawCount + LevelManager.Instance.BonusLine;
            var slowMoDuration = LevelBehaviour.Instance.SlowMotionDuration;

            for (int i = 0; i < drawChance; i++)
            {
                Instantiate(_drawChancePrefab, _drawChanceParent);
            }

            _timeSlowSlider.DOValue(0, slowMoDuration).SetEase(Ease.Linear);

            LineManager.OnLineStarted += DestroyOneChance;
        });
    }

    private void OnDisable()
    {
        _drawChanceParent.DestroyChildren();
        _timeSlowSlider.value = 0f;
        
        LineManager.OnLineStarted -= DestroyOneChance;
    }


    private void DestroyOneChance()
    {
        if (_drawChanceParent.childCount <= 0) return;

        Destroy(_drawChanceParent.GetChild(0).gameObject);
    }
}
