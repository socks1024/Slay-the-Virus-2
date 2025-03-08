using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CreatureBehaviour
{
    [HideInInspector]
    public List<CardBehaviour> deck;

    [HideInInspector]
    public BoardBehaviour board;
    
    [HideInInspector]
    public int nutrition = 0;

    /// <summary>
    /// 设置玩家的持有物
    /// </summary>
    /// <param name="cards">卡牌</param>
    /// <param name="board">棋盘</param>
    /// <param name="nutrition">货币</param>
    public void SetBackpack(List<CardBehaviour> cards, BoardBehaviour board, int nutrition)
    {
        deck = cards;
        this.board = board;
        this.nutrition = nutrition;
    }

    public override void OnBattleStart()
    {
        
    }

    public override void OnDead()
    {
        print("PlayerDead");
        EventCenter.Instance.TriggerEvent(EventType.PLAYER_DEAD);
    }
}
