using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : CardBehaviour
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
        
    }

    void Affection(CardBehaviour card)
    {
        card.nextDamage *= nextEffect;
    }
}
