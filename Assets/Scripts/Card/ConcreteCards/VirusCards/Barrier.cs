using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : CardBehaviour
{
    public override void ActOnDiscard()
    {
        ActionLib.AddVirusCardToDrawPile(this.Id, nextEffect);
        print("Discard Barrier");
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
