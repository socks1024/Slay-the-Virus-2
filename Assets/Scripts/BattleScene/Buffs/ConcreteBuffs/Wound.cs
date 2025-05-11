using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        ActionLib.WoundAction(Owner, Amount);
    }
}
