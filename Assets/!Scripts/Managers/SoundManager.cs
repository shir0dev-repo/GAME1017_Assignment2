
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
  private Dictionary<string, AudioClip> _sfxDictionary = new();
  private Dictionary<string, AudioClip> _musicDictionary = new();

  private AudioSource _sfxSource, _musicSource;

  private float _sfxVolumeCache = 0.4f, _musicVolumeCache = 0.2f, _masterVolumeCache = 0.75f;

  protected override void Awake()
  {
    base.Awake();

    _sfxSource = gameObject.AddComponent<AudioSource>();
    _musicSource = gameObject.AddComponent<AudioSource>();

    InitResourceDictionaries();
    InitAudioSources();
  }

  private void InitAudioSources()
  {
    _sfxSource.loop = false;
    _sfxSource.playOnAwake = false;

    _musicSource.loop = true;
    _musicSource.playOnAwake = true;

    SetMasterVolume(_masterVolumeCache);
    PlayMusic("Intro");
  }

  private void InitResourceDictionaries()
  {
    _musicDictionary.Add("Intro", Resources.Load<AudioClip>("Audio/Intro"));
    _musicDictionary.Add("ActionStrike", Resources.Load<AudioClip>("Audio/ActionStrike"));
    _musicDictionary.Add("GameOver", Resources.Load<AudioClip>("Audio/WakeUp"));
    _sfxDictionary.Add("Ouch", Resources.Load<AudioClip>("Audio/ManAccidentallyPunchingTheFloor"));
    _sfxDictionary.Add("Snake? Snaaake!", Resources.Load<AudioClip>("Audio/SnakeDeath"));
    _sfxDictionary.Add("Slide", Resources.Load<AudioClip>("Audio/Slide"));
    _sfxDictionary.Add("Jump", Resources.Load<AudioClip>("Audio/Jump"));
  }

  public void PlaySound(string soundKey, float volume = 1.0f)
  {
    if (!_sfxDictionary.ContainsKey(soundKey))
      throw new System.IndexOutOfRangeException(soundKey + " was not present in the sound effect dictionary!");

    _sfxSource.PlayOneShot(_sfxDictionary[soundKey], volume);
  }


  public void PlayMusic(string soundKey)
  {
    if (!_musicDictionary.ContainsKey(soundKey))
      throw new System.IndexOutOfRangeException(soundKey + " was not present in the music dictionary!");
    _musicSource.Stop();
    _musicSource.clip = _musicDictionary[soundKey];
    _musicSource.Play();
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
}