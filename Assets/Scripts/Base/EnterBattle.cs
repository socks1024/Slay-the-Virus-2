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
        EnterBattlePanel.GetComponentInChildren<EnterBattlePanel>().MissionData = missionData;
        EnterBattlePanel.SetActive(true);
        EnterBattlePanel.GetComponentInChildren<EnterBattlePanel>().Start();
    }
}
