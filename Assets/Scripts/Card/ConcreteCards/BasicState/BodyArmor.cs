using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyArmor : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
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
        ActionLib.ApplyBuffAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, "BodyArmor", nextEffect);
        ActionLib.ExhaustCardAction(this);
    }
}
