using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    /// <summary>
    /// 存储手牌的牌堆，通过这个类表现出来
    /// </summary>
    public CardPile hand;

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
    Transform drawPosition;

    [SerializeField]
    /// <summary>
    /// 弃牌的位置
    /// </summary>
    Transform discardPosition;

    void Start()
    {
        //DOTween.Init();
        hand = GetComponent<CardFlowController>().hand;
    }

    /// <summary>
    /// 为每张卡牌分配一个手牌位置和旋转
    /// </summary>
    public void ArrangeCardsInHand()
    {
        
    }

    # region hand & screen

    /// <summary>
    /// 将一张牌直接加入手牌
    /// </summary>
    public void AddCardAnim(CardBehaviour card)
    {
        card.transform.SetParent(transform, false);
        card.GetComponent<CardUI>().UIState = UIStates.HAND;
    }

    /// <summary>
    /// 将一张牌释放到屏幕空间
    /// </summary>
    public void ReleaseCardAnim(CardBehaviour card)
    {
        card.transform.parent = transform.parent;
    }

    # endregion

    # region discard

    /// <summary>
    /// 将一张牌置入弃牌堆
    /// </summary>
    /// <param name="card">要放入弃牌堆的卡</param>
    public void DiscardCardAnim(CardBehaviour card)
    {
        card.transform.parent = null;
    }

    #endregion

    # region draw

    /// <summary>
    /// 将一张牌从抽牌堆加入手牌
    /// </summary>
    public void DrawCardAnim(CardBehaviour card)
    {
        card.transform.SetParent(transform, false);
    }

    # endregion


}
