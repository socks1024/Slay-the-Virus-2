using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBuff : BuffBehaviour
{
    public override void ActOnTurnStart()
    {
        ActionLib.EnableRandomSquareAction(Amount);
        Amount -= 1;
    }
}
