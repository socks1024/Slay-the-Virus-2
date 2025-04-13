using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpareSupplies : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        ActionLib.DrawCardAction(2);
    }
}
