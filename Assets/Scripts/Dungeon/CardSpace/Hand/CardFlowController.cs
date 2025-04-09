using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    /// 弃牌堆
    /// </summary>
    public CardPile exhaustedPile = new CardPile();

    /// <summary>
    /// 手牌的上限
    /// </summary>
    public int handLimit = 10;

    /// <summary>
    /// 每回合开始时自动抽牌的数量
    /// </summary>
    [SerializeField] int autoDrawAmount = 5;

    public int AutoDrawAmount{ get{ return autoDrawAmount + DungeonManager.Instance.Player.buffOwner.GetBuffAmount("Supplies"); }}

    /// <summary>
    /// 手牌的动画处理器
    /// </summary>
    HandAnimation handAnimation;

    void Awake()
    {
        handAnimation = GetComponent<HandAnimation>();
        EventCenter.Instance.AddEventListener(EventType.TURN_START, () => { DrawCards(AutoDrawAmount); });
        EventCenter.Instance.AddEventListener(EventType.ACT_START, () => { DiscardAllCard(); });
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
        if (hand.GetCards().Contains(card))
        {
            handAnimation.DiscardCardAnim(card);
        }
    }

    /// <summary>
    /// 将所有牌置入弃牌堆
    /// </summary>
    public void DiscardAllCard()
    {
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            DiscardCard(hand.GetCards()[i]);
        }
    }

    /// <summary>
    /// 将一张卡牌消耗
    /// </summary>
    /// <param name="card">要消耗的卡牌</param>
    public void ExhaustCard(CardBehaviour card)
    {
        if (hand.GetCards().Contains(card))
        {
            hand.RemoveCard(card);
        }

        exhaustedPile.AddCard(card);
    }

    /// <summary>
    /// 抽取一张牌
    /// </summary>
    public void DrawCard()
    {
        if (hand.Count >= handLimit)
        {
            print("hand full");
            return;
        }

        if (drawPile.IsEmpty)
        {
            if (discardPile.IsEmpty)
            {
                print("empty discard pile");
                return;
            }
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
        for (int i = discardPile.Count - 1; i >= 0; --i)
        {
            drawPile.AddCard(discardPile.DrawCard());
        }
        drawPile.ShuffleCard();
    }

    /// <summary>
    /// 将一些卡牌加入抽牌堆
    /// </summary>
    /// <param name="cards">要加入的卡牌</param>
    public void FillDrawPile(List<CardBehaviour> cards)
    {
        drawPile.AddCards(cards);
        drawPile.ShuffleCard();

        foreach (CardBehaviour card in cards)
        {
            card.transform.SetParent(DungeonManager.Instance.storage);
        }
    }

}
