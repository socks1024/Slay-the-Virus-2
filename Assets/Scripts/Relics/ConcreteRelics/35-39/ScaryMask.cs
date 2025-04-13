using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryMask : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.ApplyBuffToRandomEnemyAction(Player, "Weakness", 1);
    }
}
