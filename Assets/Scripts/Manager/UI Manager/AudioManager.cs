using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    private void Awake()
    {
        Instance = this;
    }
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

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void ToggleMusic()//¾²ÒôºÍ½â³ý
    {
        musicSource.mute = !musicSource.mute;
    }

    
}
