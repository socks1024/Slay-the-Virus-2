using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessWarrior : CardBehaviour
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
        ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);
        if (cardPosition.Conditioned) ActionLib.PlayerGainVirusCard("Trap", nextEffect);
    }
}
