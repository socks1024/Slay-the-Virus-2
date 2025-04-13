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
        ActionLib.DrawCardAction(nextEffect);
        lockedOnBoard = true;
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        if (cardPosition.Conditioned)
        {
            ActionLib.ApplyBuffAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, "Wound", nextEffect);
        }
    }
}
