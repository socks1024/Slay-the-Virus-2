using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.ApplyBuffToAllEnemyAction(Player, "Wound", 3);
    }
}
