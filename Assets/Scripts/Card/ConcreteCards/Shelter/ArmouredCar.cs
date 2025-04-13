using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmouredCar : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.GainBlockAction(Player, Player, nextDefense);
        lockedOnBoard = true;
        if (cardPosition.Conditioned) ActionLib.DrawCardAction(nextEffect);
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        
    }
}
