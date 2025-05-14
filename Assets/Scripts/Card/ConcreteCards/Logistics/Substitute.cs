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

    int powerCount = 0;

    public override void ActOnCardAct()
    {
        powerCount += cardPosition.GetSatisfiedSquaresCount();
        ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);
    }

    public override void ActOnTurnStart()
    {
        nextDamage += nextEffect * powerCount;
    }
}
