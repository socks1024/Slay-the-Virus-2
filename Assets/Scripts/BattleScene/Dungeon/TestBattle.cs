using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField]List<CardBehaviour> deck;

    [SerializeField]List<RelicBehaviour> relics;

    [SerializeField]DungeonMissionData mission;

    bool startedBattle = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !startedBattle)
        {
            DialogueManager.Instance.ShowDialoguePanel()
                .AddDialogueEvent(DialogueManager.Instance.loader, new int[]{6,7,8,9,10,11,12,13,14,15,16,17,18,19})
                .ShowNextDialogueEvent();

            EnterDungeonInfo info = Messenger.enterDungeonInfo;
            if (info.p_Cards == null || info.p_Cards.Count == 0) info.p_Cards = deck;
            if (info.p_Relics == null) info.p_Relics = relics;
            if (info.missionData == null) info.missionData = mission;

            DungeonManager.Instance.StartAdventure(info);
            startedBattle = true;

            AudioManager.Instance.PlaySFX("BattleStart");
        }
    }
}
