using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : BuffBehaviour
{
    public override void ActOnTurnEnd()
    {
        Amount -= 1;
    }
}
