using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.DamageAllAction(Player, nextDamage);

        Player.nuclearBomb = true;

        ActionLib.RemoveCardFromBattle(this);
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        
    }
}
