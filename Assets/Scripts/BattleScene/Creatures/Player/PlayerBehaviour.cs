using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CreatureBehaviour
{
    public override void OnBattleStart()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();

        if (SaveSystem.Instance is not null && SaveSystem.Instance.getSave() is not null)
        {
            MaxHealth = SaveSystem.Instance.getSave().BaseLife;
        }
        
        EventCenter.Instance.AddEventListener(EventType.BATTLE_WIN, OnBattleWin);
    }

    public override void OnDead()
    {
        EventCenter.Instance.TriggerEvent(EventType.PLAYER_DEAD);
        Destroy(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_WIN, OnBattleWin);
    }

    #region Backpack

    /// <summary>
    /// 玩家所有卡牌的预制体
    /// </summary>
    [HideInInspector]public List<CardBehaviour> p_Deck = new();

    /// <summary>
    /// 玩家使用的游戏盘的预制体
    /// </summary>
    [HideInInspector]public BoardBehaviour p_Board;

    /// <summary>
    /// 玩家持有的物品的预制体
    /// </summary>
    [HideInInspector]public List<RelicBehaviour> p_Relics;

    public int Nutrition
    {
        get 
        {
            if(SaveSystem.Instance is null || SaveSystem.Instance.getSave() is null) return 0;
            return SaveSystem.Instance.getSave().Nutrient;
        }
        set 
        {
            if(SaveSystem.Instance.getSave() is not null)
            {
                if (value < 0) value = 0;
                SaveSystem.Instance.getSave().Nutrient = value; 

                OnNutritionChange.Invoke(Nutrition);
            }
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
    public void SetBackpack(List<CardBehaviour> deck, BoardBehaviour board, List<RelicBehaviour> items)
    {
        this.p_Deck.Clear();
        this.p_Deck.AddRange(deck);

        if (items != null)
        {
            this.p_Relics.Clear();
            this.p_Relics.AddRange(items);
        }

        this.p_Board = board;
    }

    #endregion

    #region Reward & Settlement

    Reward totalReward = new();

    /// <summary>
    /// 获取战斗奖励
    /// </summary>
    /// <param name="reward">奖励</param>
    public void GetRewards(Reward reward)
    {
        Nutrition += reward.money;

        if (totalReward.cardRewardInfos is null) totalReward.cardRewardInfos = new();
        if (totalReward.relicRewardInfos is null) totalReward.relicRewardInfos = new();

        totalReward.cardRewardInfos.AddRange(reward.cardRewardInfos);
        totalReward.relicRewardInfos.AddRange(reward.relicRewardInfos);
    }

    public void OnBattleWin()
    {
        buffOwner.ClearBuff();
    }

    public void OnWinLeaveDungeon()
    {
        // totalReward.cardRewardInfos.ForEach(info => SaveSystem.Instance.AddCardToPlayerSave(info.cardID, info.amount));
    }

    public void OnLoseLeaveDungeon()
    {
        // p_Deck.ForEach(card => SaveSystem.Instance.AddCardToPlayerSave(card.Id, -1));
    }

    #endregion
}
