using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : CardBehaviour
{
    public override void ActOnDiscard()
    {
        ActionLib.ApplyBuffAction(Player, Player, "Weakness", nextEffect);
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
