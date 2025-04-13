using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        base.ActOnBattleStart();
        ActionLib.ApplyBuffAction(Player, Player, "Phoenix", 1);
    }
}
