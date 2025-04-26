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

    public override void ActOnCardAct()
    {
        ActionLib.DamageAction(targetEnemy, Player, nextDamage + nextEffect * DungeonManager.Instance.battleManager.board.GetPlacedCards().Count - 1);
    }
}
