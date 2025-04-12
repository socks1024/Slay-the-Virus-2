using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microplatelet : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        if (Player.buffOwner.HasBuff("Weakness")) Player.buffOwner.GetBuff("Weakness").Amount = 0;
    }
}
