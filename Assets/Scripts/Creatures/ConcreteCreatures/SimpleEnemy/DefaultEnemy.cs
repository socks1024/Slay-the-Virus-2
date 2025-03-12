using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyBehaviour
{
    public override void OnBattleStart()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnDead()
    {
        print(name + " Dead");
        BattleManager.Instance.enemyGroup.DestroyEnemyFromBattle(this);
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
}
