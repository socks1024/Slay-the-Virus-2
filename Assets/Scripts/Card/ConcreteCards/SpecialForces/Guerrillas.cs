using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerrillas : CardBehaviour
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
        ActionLib.ApplyBuffAction(Player, Player, "Counter", nextEffect);
        if (cardPosition.Conditioned) ActionLib.ApplyBuffAction(Player, Player, "Counter", Player.takeDamage.Block / 2);
    }
}
