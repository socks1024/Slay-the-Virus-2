using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerrillas : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        ActionLib.DamageAction(targetEnemy, Player, nextDamage);
    }

    public override void ActBeforeCardAct()
    {
        base.ActBeforeCardAct();
        foreach (CardBehaviour card in cardPosition.GetCardsSatisfiedCondition())
        {
            card.nextDamage += this.nextEffect;
        }
    }
}
