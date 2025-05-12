using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaSoldier : CardBehaviour
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
        if (cardPosition.Conditioned)
        {
            ActionLib.DamageAllAction(Player, nextDamage);
        }
    }
}
