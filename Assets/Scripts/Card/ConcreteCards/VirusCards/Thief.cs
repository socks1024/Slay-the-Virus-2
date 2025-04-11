using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : CardBehaviour
{
    public override void ActOnDiscard()
    {
        ActionLib.PlayerChangeMoney( - nextEffect);
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
        ActionLib.RemoveCardFromBattle(this);
    }
}
