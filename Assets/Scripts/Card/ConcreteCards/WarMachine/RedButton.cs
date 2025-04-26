using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.DamageAllAction(Player, nextDamage);

        // List<CardBehaviour> allCards = new List<CardBehaviour>();
        // allCards.AddRange(DungeonManager.Instance.battleManager.cardFlow.hand.GetCards());
        // allCards.AddRange(DungeonManager.Instance.battleManager.cardFlow.drawPile.GetCards());
        // allCards.AddRange(DungeonManager.Instance.battleManager.cardFlow.discardPile.GetCards());
        // allCards.AddRange(DungeonManager.Instance.battleManager.cardFlow.exhaustedPile.GetCards());

        // allCards.Remove(this);

        // for (int i = 0; i < nextEffect; i++)
        // {
        //     if (allCards.Count > 0)
        //     {
        //         ActionLib.RemoveCardFromBattle(allCards[Random.Range(0,allCards.Count)]);
        //     }
        // }

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
