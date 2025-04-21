using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusTeam : EnemyBehaviour
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

        CopyIntent = new IntentionInfo(
            IntentionType.SUMMON,
            "",
            () => { ActionLib.SummonEnemyAction(this.ID);}
        );

        #endregion

        bool hasOtherTeam = false;

        foreach (EnemyBehaviour enemy in DungeonManager.Instance.battleManager.enemyGroup.enemies)
        {
            if (enemy.ID == this.ID && enemy != this)
            {
                hasOtherTeam = true;
            }
        }

        if (hasOtherTeam)
        {
            SetIntention(AttackIntent);
        }
        else
        {
            SetIntention(CopyIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int AttackDamageAmount = 4;

    IntentionInfo AttackIntent;

    IntentionInfo CopyIntent;
}
