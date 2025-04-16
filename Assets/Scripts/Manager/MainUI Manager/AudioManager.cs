using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioSourcePrototypeHolder musicManager;

    [SerializeField]
    private AudioSourcePrototypeHolder sfxManager;

    public void SetMasterVolume(float value)
    {
        SetVolume("MasterVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        SetVolume("MusicVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        SetVolume("SFXVolume", value);
    }
    public void SetVolume(string parameter,float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat(parameter, volume);
    }

    public void PlaySFX(string id)
    {
        sfxManager.PlaySound(id);
    }

    public void PlayMusic(string id)
    {
        musicManager.PlaySound(id);
    }

    public void ToggleMusic()//¾²ÒôºÍ½â³ý
    {
        musicManager.mute = !musicManager.mute;
    }

    protected override void Awake()
    {
        musicManager.audioMixer = audioMixer;
        sfxManager.audioMixer = audioMixer;
    }

    
}
