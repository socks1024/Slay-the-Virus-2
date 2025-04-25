using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyArmorBuff : BuffBehaviour
{
    public override void ActOnTurnStart()
    {
        ActionLib.GainBlockAction(Owner, DungeonManager.Instance.Player, Amount);
    }
}
