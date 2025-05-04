using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyDrop : CardBehaviour
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
        ActionLib.PlayerChangeMoney(-nextEffect);
    }
}
