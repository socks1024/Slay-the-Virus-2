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
        ActionLib.DamageAction(targetEnemy, Player, nextDamage + nextEffect * playCount);
        playCount += 1;
    }

    int playCount = 0;
}
