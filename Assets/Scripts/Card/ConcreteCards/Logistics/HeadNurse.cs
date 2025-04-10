using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadNurse : CardBehaviour
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
        ActionLib.HealAction(Player, Player, nextHeal);
        if (cardPosition.Conditioned) ActionLib.HealAction(Player, Player, nextEffect);
    }
}
