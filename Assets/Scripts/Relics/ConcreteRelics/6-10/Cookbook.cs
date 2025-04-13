using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookbook : RelicBehaviour
{
    public override void ActOnEnemyDead()
    {
        base.ActOnEnemyDead();
        ActionLib.HealAction(Player,Player,3);
    }
}
