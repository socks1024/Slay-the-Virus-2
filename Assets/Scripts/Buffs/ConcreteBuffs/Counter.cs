using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        if (Owner.buffOwner.HasBuff("CounterDummy"))
        {
            Amount -= (int)(Amount / Mathf.Pow(2,Owner.buffOwner.GetBuffAmount("CounterDummy")));
        }
        else
        {
            Amount = 0;
        }
    }
}
