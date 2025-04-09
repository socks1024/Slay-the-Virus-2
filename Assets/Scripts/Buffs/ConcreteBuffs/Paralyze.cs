using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralyze : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        Amount = 0;
    }
}
