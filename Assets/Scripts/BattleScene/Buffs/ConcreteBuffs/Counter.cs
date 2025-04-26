using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        if (!Owner.buffOwner.HasBuff("CounterDummy"))
        {
            Amount = 0;
        }
    }
}
