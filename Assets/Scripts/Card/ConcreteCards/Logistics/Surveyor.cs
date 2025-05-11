using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surveyor : CardBehaviour
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
        ActionLib.GainBlockAction(Player, Player, nextDefense);
        if (cardPosition.Conditioned) ActionLib.AddCardToDrawPile("Map", nextEffect);
    }
}
