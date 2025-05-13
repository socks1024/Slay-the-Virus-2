using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    /// <summary>
    /// 存储手牌的牌堆，通过这个类表现出来
    /// </summary>
    public CardPile Hand{ get{ return GetComponent<CardFlowController>().hand; } }

    [Header("手牌排布与动画")]
    [SerializeField]
    /// <summary>
    /// 卡牌之间间隔的距离
    /// </summary>
    float baseCardOffset = 2f;

    [SerializeField]
    /// <summary>
    /// 按照基础距离计算的卡牌数量
    /// </summary>
    int baseCardAmount = 5;

    [SerializeField]
    /// <summary>
    /// 每多一张卡牌，卡牌间隔减少的量
    /// </summary>
    float cardOffsetDecrement = 0.3f;

    /// <summary>
    /// 最终的卡牌间隔
    /// </summary>
    float CardOffset
    {
        get 
        {
            if (Hand.GetCards().Count <= baseCardAmount)
            {
                return baseCardOffset;
            }
            else
            {
                int extraCards = Hand.GetCards().Count - baseCardAmount;
                return baseCardOffset - extraCards * cardOffsetDecrement;
            }
        }
    }

    [SerializeField]
    /// <summary>
    /// 完成卡牌摆放的时间
    /// </summary>
    float arrangeTime = 2f;

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

    [SerializeField]
    /// <summary>
    /// 消耗的位置
    /// </summary>
    Transform exhaustPosition;

    void Start()
    {
        SetCardPilePanels();
    }

    /// <summary>
    /// 为每张卡牌分配一个手牌位置和旋转
    /// </summary>
    public void ArrangeCardsInHand()
    {
        List<CardBehaviour> cards = Hand.GetCards();

        for (int i = 0; i < cards.Count; i++)
        {
            CardBehaviour card = cards[i];

            Vector3 pos = transform.position;
            pos.x = transform.position.x - ( CardOffset / 2 * ( cards.Count - 1 ) ) + i * CardOffset;

            card.transform.DOComplete();

            card.transform.DOMove(pos, arrangeTime).OnComplete(
                () => {card.GetComponent<CardUI>().UIState = UIStates.HAND;}
            );
        }
    }

    //目前手牌数 count
    //卡牌间距 offset
    //基础位置 position
    //起始位置 position.x - offset * count / 2

    //pos.x += (5) * (0 - (5 - 0.5) / 2);s

    # region hand & screen

    /// <summary>
    /// 将一张牌直接加入手牌
    /// </summary>
    public void AddCardAnim(CardBehaviour card)
    {
        card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
        card.transform.SetParent(transform, false);
        ArrangeCardsInHand();
    }

    /// <summary>
    /// 将一张牌释放到屏幕空间
    /// </summary>
    public void ReleaseCardAnim(CardBehaviour card)
    {
        card.GetComponent<CardUI>().UIState = UIStates.DRAG;
        ArrangeCardsInHand();
    }

    # endregion

    # region discard

    /// <summary>
    /// 将一张牌置入弃牌堆
    /// </summary>
    /// <param name="card">要放入弃牌堆的卡</param>
    public void DiscardCardAnim(CardBehaviour card)
    {
        card.ActOnDiscard();
        card.transform.DOMove(discardPosition.position, arrangeTime).OnComplete(() => {
            card.transform.SetParent(DungeonManager.Instance.storage);
            Hand.RemoveCard(card);
            GetComponent<CardFlowController>().discardPile.AddCard(card);
            if (Hand.IsEmpty)
            {
                DungeonManager.Instance.battleManager.DiscardAnimFinished = true;
            }
        });
    }

    #endregion

    # region draw

    /// <summary>
    /// 将一张牌从抽牌堆加入手牌
    /// </summary>
    public void DrawCardAnim(CardBehaviour card)
    {
        card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
        card.transform.SetParent(transform, false);
        card.transform.position = drawPosition.position;
        ArrangeCardsInHand();
    }

    # endregion

    # region exhaust

    /// <summary>
    /// 消耗卡牌的动画
    /// </summary>
    /// <param name="card">要消耗的卡牌</param>
    public void ExhaustCardAnim(CardBehaviour card)
    {
        // card.ActOnExhaust();
        card.transform.DOMove(exhaustPosition.position, arrangeTime).OnComplete(() => {
            card.transform.SetParent(DungeonManager.Instance.storage);
            Hand.RemoveCard(card);
            GetComponent<CardFlowController>().exhaustedPile.AddCard(card);
        });
    }

    # endregion

    # region card pile

    [Header("牌堆显示窗口")]
    public ShowCardPilePanel DrawPilePanel;
    public ShowCardPilePanel DiscardPilePanel;
    public ShowCardPilePanel ExhaustedPilePanel;

    /// <summary>
    /// 初始化牌堆显示窗口
    /// </summary>
    public void SetCardPilePanels()
    {
        CardFlowController flow = GetComponent<CardFlowController>();

        DrawPilePanel.pile = flow.drawPile;
        DiscardPilePanel.pile = flow.discardPile;
        ExhaustedPilePanel.pile = flow.exhaustedPile;
    }

    public void ShowDrawPile()
    {
        ShowPile(DrawPilePanel);
    }

    public void ShowDiscardPile()
    {
        ShowPile(DiscardPilePanel);
    }

    public void ShowExhaustedPile()
    {
        ShowPile(ExhaustedPilePanel);
    }

    void ShowPile(ShowCardPilePanel panel)
    {
        panel.gameObject.SetActive(true);
        panel.ShowCards();
    }

    # endregion


}
