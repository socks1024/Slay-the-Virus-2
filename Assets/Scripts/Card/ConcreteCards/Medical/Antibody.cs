using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.RandomDamageAction(DungeonManager.Instance.Player, nextDamage);
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
