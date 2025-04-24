using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();
    [SerializeField]
    private Slider masterslider;
    [SerializeField]
    private Slider musicslider;
    [SerializeField]
    private Slider sfxslider;

    private void Start()
    {
        OnThis(panels[0]);
        SetupEventListeners();
    }

    public void OnThis(GameObject panel)
    {
        foreach(GameObject Panel in panels)
        {
            Panel.SetActive(false);
        }

        panel.SetActive(true);
    }

    private void SetupEventListeners()
    {
        masterslider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicslider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxslider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    public void ResetToDefault()
    {
        masterslider.value = 1f;
        musicslider.value = 0.8f;
        sfxslider.value = 0.8f;
    }
    
}
