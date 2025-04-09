using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steroid : CardBehaviour
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
        ActionLib.ApplyBuffAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, "Strength", nextEffect);
        ActionLib.RemoveCardFromBattle(this);
    }
}
