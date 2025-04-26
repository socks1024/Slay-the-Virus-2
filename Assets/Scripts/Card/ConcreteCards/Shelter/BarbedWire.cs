using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbedWire : CardBehaviour
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
        print(cardPosition.GetSatisfiedSquaresCount());
        for (int i = 0; i < cardPosition.GetSatisfiedSquaresCount(); i++)
        {
            ActionLib.ApplyBuffNextTurnAction(targetEnemy, Player, "Wound", nextEffect);
        }
    }
}
