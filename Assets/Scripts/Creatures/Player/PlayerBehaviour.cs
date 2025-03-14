using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CreatureBehaviour
{
    /// <summary>
    /// 玩家的卡组
    /// </summary>
    [HideInInspector]public List<CardBehaviour> deck = new List<CardBehaviour>();

    /// <summary>
    /// 玩家使用的游戏盘
    /// </summary>
    [HideInInspector]public BoardBehaviour board;

    /// <summary>
    /// 玩家持有的物品
    /// </summary>
    [HideInInspector]public List<ItemBehaviour> items;
    
    /// <summary>
    /// 玩家持有的营养货币
    /// </summary>
    [HideInInspector]public int nutrition = 0;

    /// <summary>
    /// 设置玩家的持有物
    /// </summary>
    /// <param name="deck">卡牌</param>
    /// <param name="board">棋盘</param>
    /// <param name="items">道具</param>
    /// <param name="nutrition">货币</param>
    public void SetBackpack(List<CardBehaviour> deck, BoardBehaviour board, List<ItemBehaviour> items, int nutrition)
    {
        this.deck.Clear();
        this.deck.AddRange(deck);
        this.items.Clear();
        this.items.AddRange(items);
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
