using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CreatureBehaviour
{
    /// <summary>
    /// 玩家所有卡牌的预制体
    /// </summary>
    [HideInInspector]public List<CardBehaviour> p_Deck = new List<CardBehaviour>();

    /// <summary>
    /// 玩家使用的游戏盘的预制体
    /// </summary>
    [HideInInspector]public BoardBehaviour p_Board;

    /// <summary>
    /// 玩家持有的物品的预制体
    /// </summary>
    [HideInInspector]public List<RelicBehaviour> p_Relics;
    
    /// <summary>
    /// 玩家持有的营养货币
    /// </summary>
    int nutrition = 0;
    public int Nutrition
    {
        get { return nutrition; }
        set 
        { 
            if (value < 0) value = 0;
            nutrition = value; 

            OnNutritionChange.Invoke(nutrition);
        }
    }

    public UnityAction<int> OnNutritionChange;

    /// <summary>
    /// 是否持有某种遗物
    /// </summary>
    /// <param name="id">遗物的id</param>
    /// <returns>是否有该遗物</returns>
    public bool HasRelic(string id)
    {
        foreach (RelicBehaviour relic in p_Relics)
        {
            if (relic.ID == id) return true;
        }
        return false;
    }

    /// <summary>
    /// 设置玩家的持有物
    /// </summary>
    /// <param name="deck">卡组预制体</param>
    /// <param name="board">棋盘预制体</param>
    /// <param name="items">道具预制体</param>
    /// <param name="nutrition">货币</param>
    public void SetBackpack(List<CardBehaviour> deck, BoardBehaviour board, List<RelicBehaviour> items, int nutrition)
    {
        this.p_Deck.Clear();
        this.p_Deck.AddRange(deck);

        if (items != null)
        {
            this.p_Relics.Clear();
            this.p_Relics.AddRange(items);
        }

        this.p_Board = board;
        this.nutrition = nutrition;
    }

    public override void OnBattleStart()
    {
        
    }

    public void OnBattleWin()
    {
        buffOwner.ClearBuff();
    }

    protected override void Awake()
    {
        base.Awake();
        MaxHealth = SaveSystem.Instance.getSave().BaseLife;
        EventCenter.Instance.AddEventListener(EventType.BATTLE_WIN, OnBattleWin);
    }

    public override void OnDead()
    {
        print("PlayerDead");
        EventCenter.Instance.TriggerEvent(EventType.PLAYER_DEAD);
        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_WIN, OnBattleWin);
    }
}
