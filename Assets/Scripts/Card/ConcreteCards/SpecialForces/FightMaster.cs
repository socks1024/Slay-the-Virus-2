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
        if 
        (
            targetEnemy.intention.IntentionType == IntentionType.ATTACK || 
            targetEnemy.intention.IntentionType == IntentionType.DOUBLE_ATTACK ||
            targetEnemy.intention.IntentionType == IntentionType.TRIPLE_ATTACK ||
            targetEnemy.intention.IntentionType == IntentionType.ATTACK_AND_GIVE_DEBUFF
        )
        {
            ActionLib.DamageAction(targetEnemy, Player, nextDamage);
        }
    }
}
