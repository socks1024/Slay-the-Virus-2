using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    List<GameObject> panels = new List<GameObject>();
    [SerializeField]
    Slider volumeslider;

    private void Start()
    {
        OnThis(panels[0]);
    }

    public void OnThis(GameObject panel)
    {
        foreach(GameObject Panel in panels)
        {
            Panel.SetActive(false);
        }

        panel.SetActive(true);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
