using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMaster : CardBehaviour
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
        ActionLib.DamageAction(targetEnemy, Player, nextDamage);
        playCount += 1;
    }

    public override void ActOnTurnStart()
    {
        nextDamage += nextEffect * playCount;
    }

    int playCount = 0;
}
