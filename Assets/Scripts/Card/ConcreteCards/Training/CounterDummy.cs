using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterDummy : CardBehaviour
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
        ActionLib.ApplyBuffNextTurnAction(Player, Player, "Tenacity", nextEffect);
        ActionLib.ExhaustCardAction(this);
    }
}
