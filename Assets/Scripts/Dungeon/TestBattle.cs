using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField]List<CardBehaviour> deck;

    [SerializeField]BoardBehaviour board;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DungeonManager.Instance.EnterBattleForTest(Messenger.enterBattleInfoTest);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            EnterDungeonInfo info = new EnterDungeonInfo();
            info.p_Cards = deck;
            info.p_Board = board;

            DungeonManager.Instance.StartAdventure(info);
        }
    }
}
