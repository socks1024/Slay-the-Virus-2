using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBattle : MonoBehaviour
{
    public DungeonMissionData missionData;
    public GameObject EnterBattlePanel;

    private void Start()
    {
        EnterBattlePanel.SetActive(false);
    }

    public void OnOpenEnterBattlePanel()
    {
        EnterBattlePanel.SetActive(true);
    }
}
