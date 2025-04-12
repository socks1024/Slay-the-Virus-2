using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioToxic : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        base.ActOnBattleStart();
        ActionLib.ApplyBuffToAllEnemyAction(Player, "Paralyze", 3);
    }
}
