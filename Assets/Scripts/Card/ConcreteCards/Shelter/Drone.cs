using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : CardBehaviour
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
        ActionLib.GainBlockAction(Player, Player, nextDefense);
        foreach (Square s in cardPosition.ConditionedSquares)
        {
            s.IsActive = true;
        }
        ActionLib.ExhaustCardAction(this);
    }
}
