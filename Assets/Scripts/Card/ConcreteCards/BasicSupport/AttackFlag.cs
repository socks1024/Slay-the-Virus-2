using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlag : CardBehaviour
{
    public override void ActOnDiscard()
    {
        
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

    public void Affection(CardBehaviour card)
    {
        card.nextDamage += nextEffect;
    }
}
