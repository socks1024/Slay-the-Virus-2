using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBuff: BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        Amount = 0;
    }
}
