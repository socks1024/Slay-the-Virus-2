using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeniorEngineer : CardBehaviour
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
        if (cardPosition.Conditioned)
        {
            ActionLib.GainBlockAction(targetEnemy, DungeonManager.Instance.Player, nextDefense);
        }
    }
}
