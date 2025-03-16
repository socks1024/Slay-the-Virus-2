using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        print(name + " Enter Battle");
    }

    public override void OnBattleStart()
    {
        print(name + " Battle Start");
    }

    public override void OnDead()
    {
        print(name + " Dead");
        DungeonManager.Instance.battleManager.enemyGroup.DestroyEnemyFromBattle(this);
    }

    public override void SetIntention(int turnCount)
    {
        if (turnCount % 2 == 1)
        {
            holdIntention.SetIntention("Default1");
        }
        else
        {
            holdIntention.SetIntention("Default2");
        }
    }

    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
    }

    public override void ActOnEnemyMove()
    {
        base.ActOnEnemyMove();
    }

    public override void ActOnEnemyTurnEnd()
    {
        base.ActOnEnemyTurnEnd();
    }

}


