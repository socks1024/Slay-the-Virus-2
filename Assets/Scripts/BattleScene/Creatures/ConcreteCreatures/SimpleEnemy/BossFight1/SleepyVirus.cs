using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        
    }

    public override void OnBattleStart()
    {
        
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        SnoreIntent = new IntentionInfo(
            IntentionType.ATTACK,
            new DamageInfo(Player, this, SnoreDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, SnoreDamageAmount); }
        );

        RestIntent = new IntentionInfo(
            IntentionType.HEAL,
            RestHealAmount.ToString(),
            () => { ActionLib.HealAction(this, this, RestHealAmount); }
        );

        AngerIntent = new IntentionInfo(
            IntentionType.ATTACK,
            new DamageInfo(Player, this, AngerDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, AngerDamageAmount); }
        );

        #endregion

        if (takeDamage.Health >= MaxHealth)
        {
            SetIntention(SnoreIntent);
        }
        else if (takeDamage.Health >= MaxHealth / 2)
        {
            if (turnCount % 2 == 0)
            {
                SetIntention(RestIntent);
            }
            else
            {
                SetIntention(SnoreIntent);
            }
        }
        else
        {
            if (turnCount % 2 == 0)
            {
                SetIntention(RestIntent);
            }
            else
            {
                SetIntention(AngerIntent);
            }
        }
    }

    [Header("意图相关数据")]
    [SerializeField] int SnoreDamageAmount = 4;
    [SerializeField] int RestHealAmount = 6;
    [SerializeField] int AngerDamageAmount = 8;

    IntentionInfo SnoreIntent;

    IntentionInfo RestIntent;

    IntentionInfo AngerIntent;
}
