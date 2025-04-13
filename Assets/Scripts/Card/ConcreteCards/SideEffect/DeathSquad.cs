using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSquad : CardBehaviour
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
        ActionLib.ApplyBuffAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, "Wound", nextEffect);
        if (cardPosition.Conditioned) ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage * cardPosition.GetSatisfiedSquaresCount());
    }
}
