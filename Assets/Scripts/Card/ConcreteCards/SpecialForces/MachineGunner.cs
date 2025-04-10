using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunner : CardBehaviour
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
        for (int i = 0; i < cardPosition.GetSatisfiedSquaresCount(); i++)
        {
            ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);
        }
    }
}
