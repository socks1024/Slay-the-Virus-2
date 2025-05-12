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

    public override void GetHeal(int heal)
    {
        if (HasRelic("IdolSign")) heal += 1;
        base.GetHeal(heal);
    }

    protected override void Awake()
    {
        if (SaveSystem.Instance is not null && SaveSystem.Instance.getSave() is not null)
        {
            MaxHealth = SaveSystem.Instance.getSave().BaseLife;
        }
        
        base.Awake();
        
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

    PlayerSave playerSave{ get{ return SaveSystem.Instance.getSave(); } }

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

                if (SaveSystem.Instance.getSave().Nutrient < value) AudioManager.Instance.PlaySFX("GainMoney");

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

    public bool nuclearBomb = false;

    /// <summary>
    /// 获取战斗奖励
    /// </summary>
    /// <param name="reward">奖励</param>
    public void GetRewards(Reward reward)
    {
        if (!nuclearBomb)
        {

            Nutrition += reward.money;

            if (totalReward.cardRewardInfos is null) totalReward.cardRewardInfos = new();
            if (totalReward.relicRewardInfos is null) totalReward.relicRewardInfos = new();

            totalReward.cardRewardInfos.AddRange(reward.cardRewardInfos);
            totalReward.relicRewardInfos.AddRange(reward.relicRewardInfos);

        }

        nuclearBomb = false;
    }

    public void OnBattleWin()
    {
        // buffOwner.ClearBuff();
    }

    public void OnWinLeaveDungeon()
    {
        SerializableDictionary<string,int> newPlayerHoldCards = new();

        foreach (CardBehaviour card in this.p_Deck)
        {
            if (newPlayerHoldCards.ContainsKey(card.Id))
            {
                newPlayerHoldCards[card.Id] += 1;
            }
            else
            {
                newPlayerHoldCards.Add(card.Id, 1);
            }
        }

        playerSave.PlayerHoldCards = newPlayerHoldCards;

        foreach (CardRewardInfo cardRewardInfo in this.totalReward.cardRewardInfos)
        {
            SaveSystem.Instance.AddCardToPlayerSave(cardRewardInfo.cardID, cardRewardInfo.amount);
        }
    }

    public void OnLoseLeaveDungeon()
    {
        // playerSave.PlayerHoldCards = new();
    }

    #endregion
}
