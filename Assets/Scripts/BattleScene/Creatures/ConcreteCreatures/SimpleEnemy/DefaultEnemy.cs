using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        print(name + " Enter Battle");

        defaultIntention = new IntentionInfo(
            IntentionType.STUN,
            "",
            null,
            null
        );
    }

    public override void OnBattleStart()
    {
        print(name + " Battle Start");
    }

    public override void OnDead()
    {
        base.OnDead();
        print(name + " Dead");
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        holdIntention.SetIntention(defaultIntention);
    }

    IntentionInfo defaultIntention;

}


