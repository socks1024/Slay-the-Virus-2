using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPile
{
    /// <summary>
    /// 该牌堆里存储的所有卡牌的列表
    /// </summary>
    List<CardBehaviour> cards = new List<CardBehaviour>();

    /// <summary>
    /// 该牌堆是否为空
    /// </summary>
    public bool IsEmpty{ get{ return cards.Count <= 0;}}

    /// <summary>
    /// 该牌堆牌的数量
    /// </summary>
    public int Count{ get{ return cards.Count; }}

    /// <summary>
    /// 检查牌堆中是否有某张牌
    /// </summary>
    /// <param name="id">该卡牌的ID</param>
    /// <returns>是否有该牌</returns>
    public bool HasCard(string id)
    {
        foreach (CardBehaviour card in cards)
        {
            if (card.Id == id)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 从牌堆中获取某张牌
    /// </summary>
    /// <param name="id">该卡牌的ID</param>
    /// <returns>对应的卡牌</returns>
    public CardBehaviour GetCard(string id)
    {
        foreach (CardBehaviour card in cards)
        {
            if (card.Id == id)
            {
                return card;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取所有卡牌
    /// </summary>
    /// <returns>所有卡牌</returns>
    public List<CardBehaviour> GetCards()
    {
        return cards;
    }

    /// <summary>
    /// 从牌堆中移除某张牌
    /// </summary>
    /// <param name="card">要移除的牌</param>
    public void RemoveCard(CardBehaviour card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
        }
    }

    /// <summary>
    /// 抽取当前牌堆中的第一张牌
    /// </summary>
    /// <returns>该牌堆中的第一张牌</returns>
    public CardBehaviour DrawCard()
    {
        if (!IsEmpty)
        {
            CardBehaviour card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        return null;
    }

    /// <summary>
    /// 将一张牌加入当前牌堆
    /// </summary>
    /// <param name="card">要加入的牌</param>
    public void AddCard(CardBehaviour card)
    {
        cards.Add(card);
    }

    /// <summary>
    /// 将指定的一队列卡牌加入该牌堆
    /// </summary>
    /// <param name="newCards">指定卡牌队列</param>
    public void AddCards(List<CardBehaviour> newCards)
    {
        cards.AddRange(newCards);
    }

    /// <summary>
    /// 一个简陋的洗牌算法
    /// </summary>
    public void ShuffleCard()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1); 
            CardBehaviour value = cards[r];
            cards[r] = cards[i];
            cards[i] = value;
        }
    }

    /// <summary>
    /// 清空牌堆中的卡牌
    /// </summary>
    public void ClearCards()
    {
        cards.Clear();
    }

}
