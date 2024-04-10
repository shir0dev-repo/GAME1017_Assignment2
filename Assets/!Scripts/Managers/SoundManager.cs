
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
  private Dictionary<string, AudioClip> _sfxDictionary = new();
  private Dictionary<string, AudioClip> _musicDictionary = new();

  private AudioSource _sfxSource, _musicSource;

  private float _sfxVolumeCache = 0.4f, _musicVolumeCache = 0.2f, _masterVolumeCache = 0.5f;

  private Coroutine _currentFadeInOutCO = null;

  protected override void Awake()
  {
    base.Awake();

    _sfxSource = gameObject.AddComponent<AudioSource>();
    _musicSource = gameObject.AddComponent<AudioSource>();

    InitAudioSources();
    InitResourceDictionaries();
  }

  private void InitAudioSources()
  {
    _sfxSource.loop = false;
    _sfxSource.playOnAwake = false;
    SetSFXVolume(_sfxVolumeCache);

    _musicSource.loop = true;
    _musicSource.playOnAwake = true;
    SetMusicVolume(_musicVolumeCache);

  }

  private void InitResourceDictionaries()
  {

  }

  public void PlaySound(string soundKey)
  {
    _sfxSource.PlayOneShot(_sfxDictionary[soundKey]);
  }

  public void PlayMusic(string soundKey)
  {
    if (_currentFadeInOutCO != null) return;
    _currentFadeInOutCO = StartCoroutine(MusicFadeCoroutine());
  }

  public void SetSFXVolume(float value)
  {
    _sfxVolumeCache = value;
    _sfxSource.volume = _sfxVolumeCache * _masterVolumeCache;
  }

  public void SetMusicVolume(float value)
  {
    _musicVolumeCache = value;
    _musicSource.volume = _musicVolumeCache * _masterVolumeCache;
  }

  public void SetMasterVolume(float value)
  {
    _masterVolumeCache = value;
    SetSFXVolume(_sfxVolumeCache);
    SetMusicVolume(_musicVolumeCache);
  }

  private IEnumerator MusicFadeCoroutine()
  {
    float timeElapsed = 0;
    float fadeDuration = 3f;
    while (timeElapsed < fadeDuration)
    {
      timeElapsed += Time.deltaTime;
      _musicSource.volume *= 1 - (timeElapsed / fadeDuration);
    }
  }
}