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
        if(Input.GetKeyDown(KeyCode.Space) && !startedBattle)
        {
            DungeonManager.Instance.StartAdventure(Messenger.enterDungeonInfo);
            startedBattle = true;
        }
        if(Input.GetMouseButtonDown(0) && !startedBattle)
        {
            EnterDungeonInfo info = new EnterDungeonInfo();
            info.p_Cards = deck;
            info.p_Relics = relics;
            info.missionData = mission;

            DungeonManager.Instance.StartAdventure(info);
            startedBattle = true;
        }
    }
}
