using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : CardBehaviour
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
        if (cardPosition.Conditioned) ActionLib.ApplyBuffToAllEnemyAction(DungeonManager.Instance.Player, "Stun", nextEffect);
        ActionLib.ExhaustCardAction(this);
    }
}
