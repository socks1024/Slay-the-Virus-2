using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobilization : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        ActionLib.ApplyBuffAction(Player, Player, "Strength", 1);
    }
}
