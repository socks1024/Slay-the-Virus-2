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

    public ManualPanel Terms;

    public void ShowTerms()
    {
        Terms.gameObject.SetActive(true);
        Terms.CurrContentIndex = 0;
    }
}
