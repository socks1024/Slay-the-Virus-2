using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerGroup : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        // print("PlaceCard");
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        ActionLib.GainBlockAction(targetEnemy, DungeonManager.Instance.Player, nextDefense);
        if (cardPosition.Conditioned)
        {
            ActionLib.GainBlockAction(targetEnemy, DungeonManager.Instance.Player, nextDefense);
        }
    }
}
