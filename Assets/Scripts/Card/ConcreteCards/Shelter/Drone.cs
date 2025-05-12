using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        if (cardPosition.Conditioned)
        {
            foreach (CardBehaviour card in cardPosition.GetCardsSatisfiedCondition())
            {
                DungeonManager.Instance.battleManager.cardFlow.DiscardCard(card);
            }

            ActionLib.DrawCardAction(nextEffect);
        }
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {

    }
}
