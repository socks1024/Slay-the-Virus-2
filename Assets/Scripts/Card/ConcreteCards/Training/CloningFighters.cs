using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningFighters : CardBehaviour
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
        // print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);
        for (int i = 0; i < cardPosition.GetSatisfiedSquaresCount(); i++)
        {
            ActionLib.AddCardToDrawPile("Militia",nextEffect);
        }
    }
}
