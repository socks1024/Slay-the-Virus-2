using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excavator : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        base.ActOnBattleStart();
        ActionLib.EnableRandomSquareAction(2);
    }
}
