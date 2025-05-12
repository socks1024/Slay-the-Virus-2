using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccurateAirdrop : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.DamageAction(Player, Player, nextDamage);
        ActionLib.DrawCardAction(nextEffect);
        ActionLib.ExhaustCardAction(this);
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        
    }
}
