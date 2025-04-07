using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interphone : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        if (cardPosition.GetSatisfiedSquaresCount() > 0)
        {
            ActionLib.DrawCardAction(nextEffect);
            lockedOnBoard = true;
        }
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        // print("PlayCard");
    }
}
