using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epinephrine : CardBehaviour
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
        ActionLib.RemoveCardFromBattle(this);
    }

    public override void ActBeforeCardAct()
    {
        cardPosition.GetCardsSatisfiedCondition().ForEach(c => Affection(c));
    }

    void Affection(CardBehaviour card)
    {
        card.nextDamage *= nextEffect;
        card.nextDefense *= nextEffect;
        card.nextHeal *= nextEffect;
        card.nextEffect *= nextEffect;
    }
}
