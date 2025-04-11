using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBombing : CardBehaviour
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
        ActionLib.DamageAllAction(Player, nextDamage);
        ActionLib.ExhaustCardAction(this);
    }
}
