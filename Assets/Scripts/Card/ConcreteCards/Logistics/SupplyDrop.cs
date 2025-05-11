using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyDrop : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.DrawCardAction(nextEffect);
        lockedOnBoard = true;
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        ActionLib.ExhaustCardAction(this);
    }
}
