using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snacks : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        ActionLib.HealAction(Player, Player, 1);
    }
}
