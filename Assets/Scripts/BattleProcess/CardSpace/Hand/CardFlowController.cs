using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlowController : MonoBehaviour
{
    /// <summary>
    /// 手牌
    /// </summary>
    public CardPile hand = new CardPile();

    /// <summary>
    /// 抽牌堆
    /// </summary>
    public CardPile drawPile = new CardPile();

    /// <summary>
    /// 弃牌堆
    /// </summary>
    public CardPile discardPile = new CardPile();

    /// <summary>
    /// 手牌的上限
    /// </summary>
    public int handLimit = 10;

    /// <summary>
    /// 每回合开始时自动抽牌的数量
    /// </summary>
    public int autoDrawAmount = 5;

    /// <summary>
    /// 手牌的动画处理器
    /// </summary>
    HandAnimation handAnimation;

    void Start()
    {
        handAnimation = GetComponent<HandAnimation>();
        EventCenter.Instance.AddEventListener(EventType.TURN_START, () => { DrawCards(autoDrawAmount); });
        EventCenter.Instance.AddEventListener(EventType.TURN_END, () => { DiscardAllCard(); });
    }

    /// <summary>
    /// 将一张牌直接加入手牌
    /// </summary>
    public void AddCardToHand(CardBehaviour card)
    {
        hand.AddCard(card);
        handAnimation.AddCardAnim(card);
    }

    /// <summary>
    /// 将一张牌从手牌释放到屏幕
    /// </summary>
    public void ReleaseCardFromHand(CardBehaviour card)
    {
        hand.RemoveCard(card);
        handAnimation.ReleaseCardAnim(card);
    }

    /// <summary>
    /// 从手牌丢弃一张牌
    /// </summary>
    /// <param name="card">要放入弃牌堆的卡</param>
    public void DiscardCard(CardBehaviour card)
    {
        hand.RemoveCard(card);
        discardPile.AddCard(card);
        handAnimation.DiscardCardAnim(card);
    }

    /// <summary>
    /// 将所有牌置入弃牌堆
    /// </summary>
    public void DiscardAllCard()
    {
        foreach (CardBehaviour card in hand.GetCards())
        {
            DiscardCard(card);
        }
    }

    /// <summary>
    /// 抽取一张牌
    /// </summary>
    public void DrawCard()
    {
        if (drawPile.IsEmpty)
        {
            ReshuffleDrawPileFromDiscardPile();
        }
        CardBehaviour card = drawPile.DrawCard();
        hand.AddCard(card);
        handAnimation.DrawCardAnim(card);
    }

    /// <summary>
    /// 抽取一定数量的牌
    /// </summary>
    /// <param name="amount">抽牌的数量</param>
    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DrawCard();
        }
    }

    /// <summary>
    /// 将弃牌堆中的牌加入抽牌堆，并重新洗牌
    /// </summary>
    public void ReshuffleDrawPileFromDiscardPile()
    {
        drawPile.AddCards(discardPile.GetCards());
        discardPile.ClearCards();
        drawPile.ShuffleCard();
    }

    /// <summary>
    /// 将一些卡牌加入抽牌堆
    /// </summary>
    /// <param name="cards">要加入的卡牌</param>
    public void FillDrawPile(List<CardBehaviour> cards)
    {
        drawPile.AddCards(cards);
    }

}
