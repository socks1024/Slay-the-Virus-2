using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        takeDamage.ActOnDead += OnWaiterDead;
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        ServeIntent = new IntentionInfo(
            IntentionType.HEAL,
            ServeHealAmount.ToString(),
            () => { ActionLib.HealAction(this, this, ServeHealAmount);}
        );

        #endregion

        SetIntention(ServeIntent);
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int ServeHealAmount = 8;

    IntentionInfo ServeIntent;

    void OnWaiterDead()
    {
        (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyByID("RoyalVirus") as RoyalVirus).GetAngry();
    }
}
