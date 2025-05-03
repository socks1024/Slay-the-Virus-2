using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootInfo", menuName = "ScriptableObject/LootInfo", order = 0)]
public class LootInfo : ScriptableObject
{

    /// <summary>
    /// 获得金钱的最大数量
    /// </summary>
    public int moneyVariationMax;

    /// <summary>
    /// 胜利获得金钱的最小数量
    /// </summary>
    public int moneyVariationMin;
    
    /// <summary>
    /// 战利品中所有可能出现的卡牌的预制体
    /// </summary>
    public List<CardBehaviour> cardPool;

    public int GainCardAmount;

    /// <summary>
    /// 战利品中所有可能出现的道具
    /// </summary>
    public List<RelicBehaviour> relicPool;

    public int GainRelicAmount;

    /// <summary>
    /// 随机获取金钱
    /// </summary>
    /// <returns>金钱的数量</returns>
    public int RandomGetMoney()
    {
        return Random.Range(moneyVariationMin, moneyVariationMax + 1);
    }

    /// <summary>
    /// 从卡池中抽取一定数量的卡
    /// </summary>
    /// <param name="count">抽取卡的数量</param>
    /// <returns>抽到的卡包</returns>
    public List<CardRewardInfo> RandomGetCards(int count)
    {
        List<CardRewardInfo> cardRewardInfos = new();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, cardPool.Count);

            bool newCard = true;

            for(int j = 0; j < cardRewardInfos.Count; j++)
            {
                if (cardRewardInfos[j].cardID == cardPool[index].Id)
                {
                    CardRewardInfo info = cardRewardInfos[j];
                    info.amount += 1;
                    if (CardLib.GetCardRarityType(info.cardID) == CardRarityType.UNCOMMON) info.amount += Random.Range(0, 3);
                    cardRewardInfos[j] = info;

                    newCard = false;
                }
            }

            if (newCard)
            {
                CardRewardInfo info = new();
                info.cardID = cardPool[index].Id;
                info.amount = 1;
                if (CardLib.GetCardRarityType(info.cardID) == CardRarityType.UNCOMMON) info.amount += Random.Range(0, 3);

                cardRewardInfos.Add(info);
            }
        }

        return cardRewardInfos;
    }

    /// <summary>
    /// 从道具池中抽取一定数量的道具
    /// </summary>
    /// <param name="count">抽取道具的数量</param>
    /// <returns>抽到的道具</returns>
    public List<RelicRewardInfo> RandomGetRelics(int count)
    {
        List<RelicRewardInfo> relicRewardInfos = new();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, relicPool.Count);

            bool newRelic = true;

            for(int j = 0; j < relicRewardInfos.Count; j++)
            {
                if (relicRewardInfos[j].relicID == relicPool[index].ID)
                {
                    RelicRewardInfo info = relicRewardInfos[j];
                    info.amount += 1;
                    relicRewardInfos[j] = info;

                    newRelic = false;
                }
            }

            if (newRelic)
            {
                RelicRewardInfo rewardInfo = new();
                rewardInfo.relicID = relicPool[index].ID;
                rewardInfo.amount = 1;

                relicRewardInfos.Add(rewardInfo);
            }
        }
        
        return relicRewardInfos;
    }
}

public struct CardRewardInfo
{
    public string cardID;

    public int amount;
}

public struct RelicRewardInfo
{
    public string relicID;

    public int amount;
}