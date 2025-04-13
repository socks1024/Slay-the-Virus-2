using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeArmor : CardBehaviour
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
        ActionLib.GainBlockAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextDefense);
        if (cardPosition.Conditioned) ActionLib.ApplyBuffAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, "Counter", nextEffect);
    }
}
