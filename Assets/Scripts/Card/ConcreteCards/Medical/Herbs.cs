using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbs : CardBehaviour
{
    public override void ActOnDiscard()
    {
        // print("DiscardCard");
    }

    public override void ActOnPlaced()
    {
        ActionLib.EnableRandomSquareAction(nextEffect);
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
