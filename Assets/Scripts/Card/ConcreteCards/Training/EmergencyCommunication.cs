using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyCommunication : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        foreach (CardBehaviour card in cardPosition.GetCardsSatisfiedCondition())
        {
            card.lockedOnBoard = false;
        }

        DungeonManager.Instance.battleManager.cardFlow.DiscardCard(this);
    }

    public override void ActOnRemoved()
    {
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        
    }
}
