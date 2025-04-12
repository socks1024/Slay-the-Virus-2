using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleAndThread : RelicBehaviour
{
    public override void ActOnEnemyDead()
    {
        base.ActOnEnemyDead();
        ActionLib.PlayerChangeMoney(20);
    }
}
