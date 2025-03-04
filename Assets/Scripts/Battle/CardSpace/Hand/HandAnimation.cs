using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    /// <summary>
    /// 存储手牌的牌堆，通过CardAnimation类表现出来
    /// </summary>
    public CardPile hand = new CardPile();

    // [SerializeField]
    // /// <summary>
    // /// 卡牌之间间隔的距离
    // /// </summary>
    // float cardOffset = 200f;

    // [SerializeField]
    // /// <summary>
    // /// 卡牌的最大旋转程度
    // /// </summary>
    // float rotationMax = 1f;

    // [SerializeField]
    // /// <summary>
    // /// 完成卡牌摆放的时间
    // /// </summary>
    // float arrangeTime = 2f;

    [SerializeField]
    /// <summary>
    /// 抽牌的位置
    /// </summary>
    Vector3 drawPosition;

    [SerializeField]
    /// <summary>
    /// 弃牌的位置
    /// </summary>
    Vector3 discardPosition;

    void Start()
    {
        DOTween.Init();
    }

    /// <summary>
    /// 为每张卡牌分配一个手牌位置和旋转
    /// </summary>
    public void ArrangeCardsInHand()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddCard(CardBehaviour card)
    {
        card.transform.parent.SetParent(transform);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveCard(CardBehaviour card)
    {
        card.transform.parent.SetParent(transform.parent);
    }

    /// <summary>
    /// 将一张牌置入弃牌堆
    /// </summary>
    /// <param name="card">要放入弃牌堆的卡</param>
    public void DiscardCardAnim(CardBehaviour card)
    {

    }

    /// <summary>
    /// 将所有牌置入弃牌堆
    /// </summary>
    public void DiscardAllCardAnim()
    {

    }

    /// <summary>
    /// 将一张牌从抽牌堆加入手牌
    /// </summary>
    public void DrawCardAnim()
    {

    }

    /// <summary>
    /// 将多张牌从抽牌堆加入手牌
    /// </summary>
    /// <param name="count">抽牌数目</param>
    public void DrawCardsAnim(int count)
    {

    }
}
