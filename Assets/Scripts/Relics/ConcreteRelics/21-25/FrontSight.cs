using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSight : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        base.ActOnBattleStart();
        ActionLib.ApplyBuffToAllEnemyAction(Player, "Weakness", 1);
    }
}
