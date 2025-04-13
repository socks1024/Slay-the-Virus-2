using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epinephrine : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        foreach (Square s in cardPosition.ConditionedSquares)
        {
            s.CardAdjustment += Affection;
        }
    }

    public override void ActOnRemoved()
    {
        foreach (Square s in cardPosition.ConditionedSquares)
        {
            s.CardAdjustment -= Affection;
        }
    }

    public override void ActOnCardAct()
    {
        ActionLib.RemoveCardFromBattle(this);
    }

    void Affection(CardBehaviour card)
    {
        card.nextDamage *= nextEffect;
        card.nextDefense *= nextEffect;
        card.nextHeal *= nextEffect;
        card.nextEffect *= nextEffect;
    }
}
