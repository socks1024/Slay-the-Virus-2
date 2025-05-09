using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalCommander : CardBehaviour
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

    int cardAmount = 0;

    public override void ActOnCardAct()
    {
        ActionLib.DamageAction(targetEnemy, Player, nextDamage + nextEffect * cardAmount);
        cardAmount = 0;
    }

    public override void ActBeforeCardAct()
    {
        base.ActBeforeCardAct();
        cardAmount = DungeonManager.Instance.battleManager.board.GetPlacedCards().Count - 1;
    }
}
