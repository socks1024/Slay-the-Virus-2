using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuy : CardBehaviour
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
        ActionLib.GainBlockAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextDefense);
        if (cardPosition.Conditioned) ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);
    }
}
