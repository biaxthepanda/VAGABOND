using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : PersistentSingleton<SoundController>
{
    [SerializeField]
    private AudioMixer _mixer;
    
    [SerializeField]
    private List<AudioClip> _musics;

    [SerializeField]
    private List<AudioSource> _musicPlayer;

    private int _selectedMusicPlayer = 0;
    
    [SerializeField]
    private List<AudioClip> _effects;
    [SerializeField]
    private AudioSource _effectPlayer;

    private void Start()
    {
        foreach (var audioSource in _musicPlayer)
        {
            audioSource.volume = 0;
            audioSource.loop = true;
            audioSource.Stop();
        }

        _effectPlayer.volume = 1;
        _effectPlayer.loop = false;
        _effectPlayer.Stop();
    }

    public void PlayMusic(Musics music)
    {
        if (_musicPlayer[_selectedMusicPlayer].isPlaying)
        {
            if (_musicPlayer[_selectedMusicPlayer].clip.Equals(_musics[(int)music])) return;
            
            _musicPlayer[_selectedMusicPlayer].DOFade(0, 3);
            
            var fadingPlayer = _selectedMusicPlayer;
            DOVirtual.DelayedCall(3.01f, () =>
            {
                _musicPlayer[fadingPlayer].Stop();
            });
            
            SwitchSelectedPlayer();
            _musicPlayer[_selectedMusicPlayer].clip = _musics[(int)music];
            _musicPlayer[_selectedMusicPlayer].PlayDelayed(0.1f);
            _musicPlayer[_selectedMusicPlayer].DOFade(1, 3);
            
        }
        else
        {
            _musicPlayer[_selectedMusicPlayer].clip = _musics[(int)music];
            
            
            DOVirtual.DelayedCall(1f, () =>
            {
                _musicPlayer[_selectedMusicPlayer].Play();
                _musicPlayer[_selectedMusicPlayer].DOFade(1, 3);
            });
        }
    }
    
    public void PlaySFX(SoundEffects effect)
    {
        _effectPlayer.clip = _effects[(int)effect];
    }

    private void SwitchSelectedPlayer()
    {
        _selectedMusicPlayer = _selectedMusicPlayer == 1 ? 0 : 1;
    }

    public void SetMaster(float value)
    {
        _mixer.SetFloat("Master", value);
    }
    public void SetMusic(float value)
    {
        _mixer.SetFloat("Music", value);
    }
    public void SetSFX(float value)
    {
        _mixer.SetFloat("SFX", value);
    }

    [Serializable]
    public enum Musics
    {
        Menu = 0,
        Idle = 1,
        Combat = 2,
        BossMusic = 3,
    }
    
    [Serializable]
    public enum SoundEffects
    {
        Swoosh = 0,
        SwordIn = 1,
        Blood = 2,
        Walking = 3,
    }
}
