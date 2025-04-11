using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : CardBehaviour
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
        ActionLib.ApplyBuffAction(targetEnemy, Player, "Wound", nextEffect);
        if (cardPosition.Conditioned) targetEnemy.buffOwner.GetBuff("Wound").Amount *= 2;
    }
}
