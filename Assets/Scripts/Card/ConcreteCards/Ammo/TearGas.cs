using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearGas : CardBehaviour
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
        ActionLib.ApplyBuffAction(targetEnemy, DungeonManager.Instance.Player, "Paralyze", nextEffect);
        if (cardPosition.Conditioned) ActionLib.ApplyBuffAction(targetEnemy, DungeonManager.Instance.Player, "Paralyze", nextEffect);
    }
}
