using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIntention : IntentionBehaviour
{
    public override void ActOnEnemyTurn()
    {
        ActionLib.DamageAction(target, source, Amount);
    }
}
