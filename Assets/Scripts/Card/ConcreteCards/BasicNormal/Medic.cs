using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : CardBehaviour
{
    public override void ActOnDiscard()
    {
        print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        // print("PlaceCard");
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        ActionLib.HealAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextHeal);
        ActionLib.ExhaustCardAction(this);
    }
}
