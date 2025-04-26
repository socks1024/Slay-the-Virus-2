using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarShip : CardBehaviour
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
        if (cardPosition.Conditioned) ActionLib.GainBlockAction(Player, Player, nextDefense);
    }

    public override void ActAfterCardAct()
    {
        ActionLib.DamageAction(targetEnemy, Player, Player.takeDamage.Block + nextDamage);
    }
}
