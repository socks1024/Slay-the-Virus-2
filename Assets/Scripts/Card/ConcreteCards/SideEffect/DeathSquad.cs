using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSquad : CardBehaviour
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

    int virusCardCount = 0;

    public override void ActOnCardAct()
    {
        ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage + nextEffect * virusCardCount);
        virusCardCount = 0;
    }

    public override void ActBeforeCardAct()
    {
        base.ActBeforeCardAct();
        foreach (CardBehaviour card in DungeonManager.Instance.battleManager.board.GetPlacedCards())
        {
            if (card.ActType == CardActType.VIRUS)
            {
                virusCardCount += 1;
            }
        }
    }
}
