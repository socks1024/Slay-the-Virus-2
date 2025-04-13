using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardiac : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        ActionLib.HealAction(Player, Player, 2);
    }
}
