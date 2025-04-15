using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAttackIntention : IntentionBehaviour
{
    public override void ActOnEnemyTurn()
    {
        ActionLib.MultiAttackAction(target, source, Amount, 2);
    }
}
