using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knifeman : CardBehaviour
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
        ActionLib.DamageAction(targetEnemy, Player, nextDamage);
        if (cardPosition.Conditioned) ActionLib.ApplyBuffNextTurnAction(targetEnemy, DungeonManager.Instance.Player, "Wound", nextEffect);
    }
}
