using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSoldier : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        lockedOnBoard = true;
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        ActionLib.GainBlockAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextDefense);
    }
}
