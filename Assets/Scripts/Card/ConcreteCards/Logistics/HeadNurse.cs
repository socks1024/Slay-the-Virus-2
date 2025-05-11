using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadNurse : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        for (int i = 0; i < cardPosition.GetSatisfiedSquaresCount(); i++)
        {
            ActionLib.AddCardToHand("Medic", nextEffect);
        }

        lockedOnBoard = true;
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        
    }
}
