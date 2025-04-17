using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField]List<CardBehaviour> deck;

    [SerializeField]List<RelicBehaviour> relics;

    [SerializeField]DungeonMissionData mission;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DungeonManager.Instance.StartAdventure(Messenger.enterDungeonInfo);
        }
        if(Input.GetMouseButtonDown(0))
        {
            EnterDungeonInfo info = new EnterDungeonInfo();
            info.p_Cards = deck;
            info.p_Relics = relics;
            info.missionData = mission;

            DungeonManager.Instance.StartAdventure(info);
        }
    }
}
