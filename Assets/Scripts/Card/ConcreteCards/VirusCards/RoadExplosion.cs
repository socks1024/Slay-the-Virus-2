using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadExplosion : CardBehaviour
{
    public override void ActOnDiscard()
    {
        ActionLib.ApplyBuffNextTurnAction(Player, Player, "RoadExplosion", nextEffect);
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
        ActionLib.RemoveCardFromBattle(this);
    }
}
