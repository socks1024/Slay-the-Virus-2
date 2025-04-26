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
        
    }

    public override void ActOnRemoved()
    {
        
    }

    public override void ActOnCardAct()
    {
        ActionLib.GainBlockAction(DungeonManager.Instance.Player, DungeonManager.Instance.Player, nextDefense);
    }

    public override void ActBeforeCardAct()
    {
        cardPosition.GetCardsSatisfiedCondition().ForEach(c => Affection(c));
    }

    void Affection(CardBehaviour card)
    {
        card.nextDamage = 0;
    }
}
