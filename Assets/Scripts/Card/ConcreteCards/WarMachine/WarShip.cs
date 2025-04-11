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
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, Smash);
        if (cardPosition.Conditioned) ActionLib.GainBlockAction(Player, Player, nextDefense);
    }

    void Smash()
    {
        ActionLib.DamageAction(targetEnemy, Player, Player.takeDamage.Block + nextDamage);
        EventCenter.Instance.RemoveEventListener(EventType.ENEMY_ACT_END, Smash);
    }
}
