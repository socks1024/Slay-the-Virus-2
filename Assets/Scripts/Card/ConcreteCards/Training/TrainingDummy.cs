using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : CardBehaviour
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
        ActionLib.ApplyBuffAction(Player, Player, "Strength", nextEffect);
        ActionLib.ExhaustCardAction(this);
    }
}
