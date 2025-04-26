using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBuff : BuffBehaviour
{
    public BuffOnTurnEndBehaviour turnEndBehaviour;

    public override void ActOnTurnEnd()
    {
        if (turnEndBehaviour == BuffOnTurnEndBehaviour.NONE)
        {
            return;
        }
        if (turnEndBehaviour == BuffOnTurnEndBehaviour.LOSE_ONE)
        {
            Amount -= 1;
        }
        if (turnEndBehaviour == BuffOnTurnEndBehaviour.LOSE_ALL)
        {
            Amount = 0;
        }
    }
}

public enum BuffOnTurnEndBehaviour
{
    NONE,
    LOSE_ONE,
    LOSE_ALL,
}
