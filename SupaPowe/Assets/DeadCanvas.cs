using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DeadCanvas : MonoBehaviour
{
    [SerializeField]
    private Image _deadImage;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnStateChange;
    }
    private void OnStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Lose)
        {
            DOVirtual.DelayedCall(2f, () =>
            {
                _deadImage.DOFade(1f, 3f).SetEase(Ease.OutExpo).OnComplete((() =>
                {
                    _deadImage.DOFade(0f, 1f).SetEase(Ease.Linear);
                }));
                
            });

        }
    }

}
