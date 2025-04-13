using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBag : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.HealAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextHeal);
        ActionLib.RemoveCardFromBattle(this);
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        
    }
}
