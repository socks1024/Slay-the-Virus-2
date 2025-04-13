using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substitute : CardBehaviour
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
        ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);
        if (cardPosition.Conditioned) ActionLib.ApplyBuffAction(Player, Player, "SystemEngineer", nextEffect);
    }
}
