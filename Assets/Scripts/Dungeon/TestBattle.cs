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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DungeonManager.Instance.EnterBattleForTest(deck, board);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DungeonManager.Instance.EnterBattleForTest(Messenger.enterBattleInfoTest);
        }
    }
}
