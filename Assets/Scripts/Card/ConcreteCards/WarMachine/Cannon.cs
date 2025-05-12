using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        foreach (CardBehaviour card in cardPosition.GetCardsSatisfiedCondition())
        {
            for (int i = 0; i < nextEffect; i++)
            {
                card.ActOnCardAct();
            }
        }

        ActionLib.ExhaustCardAction(this);
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        
    }
}
