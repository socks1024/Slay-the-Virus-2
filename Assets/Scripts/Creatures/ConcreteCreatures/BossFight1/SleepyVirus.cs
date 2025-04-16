using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        SleepIntent = new IntentionInfo(
            IntentionType.DEFENSE,
            SleepBlockAmount.ToString(),
            null,
            () => { ActionLib.GainBlockAction(this, this, SleepBlockAmount); }
        );

        RestIntent = new IntentionInfo(
            IntentionType.HEAL,
            RestHealAmount.ToString(),
            () => { ActionLib.HealAction(this, this, RestHealAmount); },
            null
        );

        AngerIntent = new IntentionInfo(
            IntentionType.ATTACK,
            AngerDamageAmount.ToString(),
            () => { ActionLib.DamageAction(Player, this, AngerDamageAmount); },
            null
        );
    }

    public override void OnBattleStart()
    {
        
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        if (takeDamage.Health >= MaxHealth)
        {
            SetIntention(SleepIntent);
        }
        else if (takeDamage.Health >= MaxHealth / 2)
        {
            if (turnCount % 2 == 0)
            {
                SetIntention(RestIntent);
            }
            else
            {
                SetIntention(SleepIntent);
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
    [SerializeField] int SleepBlockAmount = 6;
    [SerializeField] int RestHealAmount = 6;
    [SerializeField] int AngerDamageAmount = 8;

    IntentionInfo SleepIntent;

    IntentionInfo RestIntent;

    IntentionInfo AngerIntent;
}
