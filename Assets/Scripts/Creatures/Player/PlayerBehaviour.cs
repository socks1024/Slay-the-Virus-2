using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CreatureBehaviour
{
    [HideInInspector]
    public List<CardBehaviour> deck = new List<CardBehaviour>();

    [HideInInspector]
    public BoardBehaviour board;

    //public List<ItemBehaviour> items;
    
    [HideInInspector]
    public int nutrition = 0;

    /// <summary>
    /// 设置玩家的持有物
    /// </summary>
    /// <param name="deck">卡牌</param>
    /// <param name="board">棋盘</param>
    /// <param name="nutrition">货币</param>
    public void SetBackpack(List<CardBehaviour> deck, BoardBehaviour board, int nutrition)
    {
        this.deck.Clear();
        this.deck.AddRange(deck);
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
