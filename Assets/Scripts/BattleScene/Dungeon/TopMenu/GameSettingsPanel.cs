using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingsPanel : MonoBehaviour
{
    public void HideSettingsPanel()
    {
        gameObject.SetActive(false);
    }

    public void LeaveBattle()
    {
        DungeonManager.Instance.SettingLeaveDungeon();
    }

    public void ShowTerms()
    {
        ManualPanel.ShowPanel();
    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideSettingsPanel();
        }
    }
}
