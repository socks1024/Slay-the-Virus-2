using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        AttackIntent = new IntentionInfo(
            IntentionType.ATTACK,
            new DamageInfo(Player, this, AttackDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, AttackDamageAmount);}
        );

        GuardIntent = new IntentionInfo(
            IntentionType.HEAL,
            GuardHealAmount.ToString(),
            () => { ActionLib.HealAction(DungeonManager.Instance.battleManager.enemyGroup.GetRandomEnemy(), this, GuardHealAmount);}
        );

        #endregion

        if (Random.value < 0.5f)
        {
            SetIntention(AttackIntent);
        }
        else
        {
            SetIntention(GuardIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int AttackDamageAmount = 4;
    [SerializeField] int GuardHealAmount = 6;

    IntentionInfo AttackIntent;

    IntentionInfo GuardIntent;
}
