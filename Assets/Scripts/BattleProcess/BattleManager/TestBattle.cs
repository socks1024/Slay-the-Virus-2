using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    /// <summary>
    /// 卡组
    /// </summary>
    public List<CardBehaviour> cards;

    /// <summary>
    /// 棋盘
    /// </summary>
    public BoardBehaviour board;

    /// <summary>
    /// 所有敌人
    /// </summary>
    public List<EnemyBehaviour> enemies;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BattleManager.Instance.player.SetBackpack(cards, board, 0);
            BattleManager.Instance.InitializeEncounter(enemies);
        }
    }

}
