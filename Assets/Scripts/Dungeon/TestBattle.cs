using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    [SerializeField]List<CardBehaviour> deck;

    [SerializeField]BoardBehaviour board;

    [SerializeField]List<EnemyBehaviour> enemies;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DungeonManager.Instance.EnterBattleForTest(deck, board, enemies);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DungeonManager.Instance.EnterBattleForTest(Messenger.enterBattleInfoTest);
        }
    }
}
