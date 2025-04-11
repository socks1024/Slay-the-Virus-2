using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyCommunication : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.GetRandomCardFromExhaustPile(nextEffect);
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
