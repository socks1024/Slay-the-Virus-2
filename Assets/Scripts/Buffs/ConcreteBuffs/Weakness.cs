using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        Amount -= 1;
    }
}
