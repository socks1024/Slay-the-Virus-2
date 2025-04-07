using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCard : CardBehaviour
{
    public override void ActOnDiscard()
    {
        print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        print("PlaceCard");
    }

    public override void ActOnRemoved()
    {
        print("RemoveCard");
    }

    public override void ActOnCardAct()
    {
        ActionLib.DamageAction(targetEnemy, DungeonManager.Instance.Player, nextDamage);

        if (cardPosition.GetSatisfiedSquaresCount() > 0)
        {
            print("Triggered");
        }

        if (cardPosition.GetCardsSatisfiedCondition().Count > 0)
        {
            cardPosition.GetCardsSatisfiedCondition().ForEach(x => {print(x.Id);});
        }
    }
}
