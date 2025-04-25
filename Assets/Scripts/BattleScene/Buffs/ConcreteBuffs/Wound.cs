using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        print(Owner);
        ActionLib.WoundAction(Owner, Amount);
        Amount = 0;
    }
}
