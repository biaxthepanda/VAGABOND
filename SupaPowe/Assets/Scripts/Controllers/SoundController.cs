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
    
    [SerializeField]
    private List<AudioClip> _walkSounds;
    [SerializeField]
    private List<AudioClip> _clashSounds;
    [SerializeField]
    private AudioSource _walkPlayer;
    [SerializeField]
    private AudioSource _windPlayer;
    public float windTimer;
    public float windCountdown = 5;
    
    [SerializeField]
    private AudioClip _dialogueClip1;
    [SerializeField]
    private AudioClip _dialogueClip2;
    [SerializeField]
    private AudioSource _dialoguePlayer;

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
        
        _walkPlayer.volume = 1;
        _walkPlayer.loop = false;
        _walkPlayer.Stop();
        
        
        SetMaster(PlayerPrefs.GetFloat("MasterSound", 0));
        SetMusic(PlayerPrefs.GetFloat("MusicSound", 0));
        SetSFX(PlayerPrefs.GetFloat("SFXSound", 0), false);
    }

    private void Update()
    {
        if(windTimer <= windCountdown)
        {
            windTimer += Time.deltaTime;
        }
        else
        {
            windCountdown = UnityEngine.Random.Range(9, 15);
            windTimer = 0;
            _windPlayer.DOKill();
            _windPlayer.DOFade(0,2);
            DOVirtual.DelayedCall(2.5f, () => _windPlayer.DOFade(0.05f,2f));
        }
    }
    private Tween tween;
    public void PlayMusic(Musics music)
    {
        if (_musicPlayer[_selectedMusicPlayer].isPlaying)
        {
            if (_musicPlayer[_selectedMusicPlayer].clip.Equals(_musics[(int)music]))
            {
                _musicPlayer[_selectedMusicPlayer].volume = 1;
                return;
            }

            this.DOKill();
            _musicPlayer[0].DOKill();
            _musicPlayer[1].DOKill();
            tween.Kill();
            
            _musicPlayer[_selectedMusicPlayer].DOFade(0, 3);
            
            var fadingPlayer = _selectedMusicPlayer;
            tween = DOVirtual.DelayedCall(3.01f, () =>
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
        if (effect == SoundEffects.Walking)
        {
            PlayWalk();
            return;
        }else if(effect == SoundEffects.SwordClash)
        {
            PlaySwordClash();
            return;
        }
        _effectPlayer.PlayOneShot(_effects[(int)effect]);
    }
    public void PlayDialogue(int boss)
    {
        var clip = boss == 0 ? _dialogueClip1 : _dialogueClip2;
        
        _dialoguePlayer.PlayOneShot(clip);
        
        PlayMusic(Musics.BossMusic);
    }

    private void SwitchSelectedPlayer()
    {
        _selectedMusicPlayer = _selectedMusicPlayer == 1 ? 0 : 1;
    }
    
    
    private void PlayWalk()
    {
		if (_walkPlayer.isPlaying) return;
        _walkPlayer.PlayOneShot(_walkSounds.Rand());
    }
    private void PlaySwordClash()
    {
        _effectPlayer.PlayOneShot(_clashSounds.Rand());
    }

    public void SetMaster(float value)
    {
        _mixer.SetFloat("Master", value);
        PlayerPrefs.SetFloat("MasterSound", value);
    }
    public void SetMusic(float value)
    {
        _mixer.SetFloat("Music", value);
        PlayerPrefs.SetFloat("MusicSound", value);
    }
    public void SetSFX(float value, bool playSFXDemo = true)
    {
        _mixer.SetFloat("SFX", value);
        PlayerPrefs.SetFloat("SFXSound", value);
        
        if (playSFXDemo && !_effectPlayer.isPlaying)
            PlaySFX(SoundEffects.Swoosh);
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
        DamageFirst = 4,
        DamageSecond = 5,
        SwordClash = 6,
        Death = 7,
        Shuriken = 8,
    }
}
