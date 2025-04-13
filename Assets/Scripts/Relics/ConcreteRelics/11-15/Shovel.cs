using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        ActionLib.GainBlockAction(Player, Player, 5);
    }
}
