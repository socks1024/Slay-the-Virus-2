using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancePolicy : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        // print("PlaceCard");
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        foreach (Square s in cardPosition.ConditionedSquares)
        {
            s.IsActive = true;
        }

        ActionLib.ExhaustCardAction(this);
    }
}
