using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : CardBehaviour
{
    public override void ActOnDiscard()
    {
        ActionLib.ApplyBuffAction(Player, Player, "Wound", nextEffect);
        ActionLib.ApplyBuffAction(Player, Player, "Paralyze", nextEffect);
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
