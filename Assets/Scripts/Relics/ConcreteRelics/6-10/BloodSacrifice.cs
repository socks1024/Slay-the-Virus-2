using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSacrifice : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        ActionLib.DirectlyChangeHealthAction(Player, 4);
        ActionLib.ApplyBuffAction(Player, Player, "Strength", 1);
    }
}
