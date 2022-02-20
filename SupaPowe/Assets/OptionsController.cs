using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

    [SerializeField]
    private Slider _masterSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _sfxSlider;

    private void Awake()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterSound", 0);
        _musicSlider.value = PlayerPrefs.GetFloat("MusicSound", 0);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXSound", 0);
    }
    public void SetMaster()
    {
        SoundController.Instance.SetMaster(_masterSlider.value);
    }
    public void SetMusic()
    {
        SoundController.Instance.SetMusic(_musicSlider.value);
    }
    public void SetSFX()
    {
        SoundController.Instance.SetSFX(_sfxSlider.value);
    }
}
