using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPile
{
    /// <summary>
    /// 该牌堆里存储的所有卡牌的列表
    /// </summary>
    Queue<CardBehaviour> cards;

    /// <summary>
    /// 该牌堆是否为空
    /// </summary>
    public bool IsEmpty{ get{ return cards.Count > 0;}}

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
    /// 抽取当前牌堆中的第一张牌
    /// </summary>
    /// <returns>该牌堆中的第一张牌</returns>
    public CardBehaviour DrawCard()
    {
        return cards.Dequeue();
    }

    /// <summary>
    /// 将一张牌加入当前牌堆
    /// </summary>
    /// <param name="card">要加入的牌</param>
    public void AddCard(CardBehaviour card)
    {
        cards.Enqueue(card);
    }

    /// <summary>
    /// 抽取指定数量张牌
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <returns>包含指定数量张牌的队列</returns>
    public Queue<CardBehaviour> DrawCards(int count)
    {
        Queue<CardBehaviour> retCards = new Queue<CardBehaviour>();
        for (int i = 0; i < count; i++)
        {
            retCards.Enqueue(cards.Dequeue());
        }
        return retCards;
    }

    /// <summary>
    /// 将指定的一队列卡牌加入该牌堆
    /// </summary>
    /// <param name="newCards">指定卡牌队列</param>
    public void AddCards(Queue<CardBehaviour> newCards)
    {
        foreach (CardBehaviour newCard in newCards)
        {
            cards.Enqueue(newCard);
        }
    }

    /// <summary>
    /// 一个简陋的Queue洗牌算法
    /// </summary>
    public void ShuffleCard()
    {
        CardBehaviour[] tempCards = cards.ToArray();

        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            CardBehaviour tempCard = tempCards[i];
            tempCards[i] = tempCards[j];
            tempCards[j] = tempCard;
        }

        cards.Clear();
        
        foreach (CardBehaviour card in tempCards)
        {
            cards.Enqueue(card);
        }
    }

    /// <summary>
    /// 清空牌堆中的卡牌
    /// </summary>
    public void ClearCards()
    {
        cards.Clear();
    }

    /// <summary>
    /// 获取所有卡牌
    /// </summary>
    /// <returns>所有卡牌</returns>
    public Queue<CardBehaviour> GetCards()
    {
        return cards;
    }

}
