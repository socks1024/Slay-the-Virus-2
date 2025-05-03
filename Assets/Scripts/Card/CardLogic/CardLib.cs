using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardLib
{
    /// <summary>
    /// 所有卡牌存储
    /// </summary>
    public static Dictionary<string, CardBehaviour> cardPrefabs = new Dictionary<string, CardBehaviour>();

    /// <summary>
    /// 获取某张卡牌
    /// </summary>
    /// <param name="id">卡牌的ID</param>
    /// <returns>卡牌的预制体</returns>
    public static CardBehaviour GetCard(string id)
    {
        return cardPrefabs[id];
    }

    public static CardRarityType GetCardRarityType(string id)
    {
        return cardPrefabs[id].RarityType;
    }

    /// <summary>
    /// 初始化卡牌
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    public static void InitSetCards()
    {
        LoadCard();
    }

    /// <summary>
    /// 添加卡牌
    /// </summary>
    /// <param name="name">卡牌的路径</param>
    static void LoadCard()
    {
        CardBehaviour[] cards = Resources.LoadAll<CardBehaviour>("Prefabs/Card/ConcreteCards");
        foreach (CardBehaviour card in cards)
        {
            cardPrefabs.Add(card.Id, card);
        }
    }
}
