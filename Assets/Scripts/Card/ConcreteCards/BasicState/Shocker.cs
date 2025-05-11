using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : CardBehaviour
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
        ActionLib.GainBlockAction(Player, Player, nextDefense);
        if (cardPosition.Conditioned) ActionLib.ApplyBuffAction(targetEnemy, DungeonManager.Instance.Player, "Paralyze", nextEffect);
    }
}
