using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.HealAction(Player, Player, nextHeal);
        if (cardPosition.Conditioned) ActionLib.ClearHighestDebuffAction(Player);
        ActionLib.ExhaustCardAction(this);
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        
    }
}
