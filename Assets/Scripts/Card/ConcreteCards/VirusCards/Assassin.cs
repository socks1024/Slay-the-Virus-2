using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : CardBehaviour
{
    public override void ActOnDiscard()
    {
        ActionLib.DamageAction(Player, null, nextDamage);
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
