using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissalSeal : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        cardPosition.GetCardsSatisfiedCondition().ForEach(card => ActionLib.ExhaustCardAction(card));
        ActionLib.ExhaustCardAction(this);
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        
    }
}
