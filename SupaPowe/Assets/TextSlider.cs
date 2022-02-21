using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextSlider : MonoBehaviour
{
    [SerializeField]
    private RectTransform _transform;
    
    [SerializeField]
    private AudioSource _audioSource;
    
    [SerializeField]
    private Transform _target;
    
    [SerializeField]
    private float _duration;
    void Start()
    {
        _transform.DOMove(_target.position, _duration).OnComplete((() =>
        {
            _audioSource.DOFade(0, 3);
            _transform.GetComponent<TextMeshProUGUI>().DOFade(0, 3).OnComplete((() =>
            {
                SceneManager.LoadScene("Main");
            }));
        }));
    }

}
