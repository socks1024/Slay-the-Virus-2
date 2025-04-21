using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusFollower : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        AttackIntent = new IntentionInfo(
            IntentionType.ATTACK,
            AttackDamageAmount.ToString(),
            () => { ActionLib.DamageAction(Player, this, AttackDamageAmount); }
        );

        DedicationIntent = new IntentionInfo(
            IntentionType.HEAL,
            DedicationHealAmount.ToString(),
            () => { ActionLib.HealAction(DungeonManager.Instance.battleManager.enemyGroup.GetEnemyByID("VirusKing"), this, DedicationHealAmount);}
        );

        #endregion

        if (Random.value > 0.5f)
        {
            SetIntention(AttackIntent);
        }
        else
        {
            SetIntention(DedicationIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    IntentionInfo AttackIntent;

    [Header("意图相关数据")]
    [SerializeField] int AttackDamageAmount = 4;

    IntentionInfo DedicationIntent;
    [SerializeField] int DedicationHealAmount = 6;
}
