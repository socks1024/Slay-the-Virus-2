using System.Collections.Generic;
using Timers;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField]List<CardBehaviour> deck;

    [SerializeField]List<RelicBehaviour> relics;

    [SerializeField]DungeonMissionData mission;

    void Start()
    {
        TimersManager.SetTimer("StartBattle", 0.5f, StartBattle);
    }

    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0) && !startedBattle)
    //     {
    //         StartBattle();
    //     }
    // }

    void StartBattle()
    {
        if (SaveSystem.Instance != null && !SaveSystem.Instance.getSave().TutorialClear[2])
        {
            SaveSystem.Instance.SetTutorialClear(2);

            DialogueManager.Instance.ShowDialoguePanel()
                .AddDialogueEvent(DialogueManager.Instance.loader, "battle")
                .ShowNextDialogueEvent()
                .AddOnEventEndCallback(ShowManual);
        }

        EnterDungeonInfo info = Messenger.enterDungeonInfo;

        if (info.p_Cards == null || info.p_Cards.Count == 0) info.p_Cards = deck;
        if (info.p_Relics == null) info.p_Relics = relics;
        if (info.missionData == null) info.missionData = mission;

        DungeonManager.Instance.StartAdventure(info);

        AudioManager.Instance.PlaySFX("BattleStart");
    }

    void ShowManual()
    {
        ManualPanel.ShowPanel();
    }
}
