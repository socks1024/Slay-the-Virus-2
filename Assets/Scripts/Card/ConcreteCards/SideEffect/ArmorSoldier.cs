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
        foreach (Square s in cardPosition.ConditionedSquares)
        {
            s.CardAdjustment += Affection;
        }
    }

    public override void ActOnRemoved()
    {
        foreach (Square s in cardPosition.ConditionedSquares)
        {
            s.CardAdjustment -= Affection;
        }
    }

    public override void ActOnCardAct()
    {
        ActionLib.GainBlockAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextDefense);
    }

    void Affection(CardBehaviour card)
    {
        card.nextDamage = 0;
    }
}
