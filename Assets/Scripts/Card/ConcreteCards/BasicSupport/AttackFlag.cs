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
        
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        
    }

    public override void ActBeforeCardAct()
    {
        cardPosition.GetCardsSatisfiedCondition().ForEach(c => Affection(c));
    }

    public void Affection(CardBehaviour card)
    {
        card.nextDamage += nextEffect;
    }
}
