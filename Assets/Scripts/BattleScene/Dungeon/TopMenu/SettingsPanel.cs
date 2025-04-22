using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanel : MonoBehaviour
{
    public void HideSettingsPanel()
    {
        gameObject.SetActive(false);
    }

    public void LeaveBattle()
    {
        SceneManager.LoadScene("Base");
    }

    public void ShowSettings()
    {
        
    }
}
