using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        SnoreIntent = new IntentionInfo(
            IntentionType.GIVE_DEBUFF,
            SleepBuffAmount.ToString(),
            () => { ActionLib.ApplyBuffAction(Player, this, "Paralyze", SleepBuffAmount); }
        );

        RestIntent = new IntentionInfo(
            IntentionType.HEAL,
            RestHealAmount.ToString(),
            () => { ActionLib.HealAction(this, this, RestHealAmount); }
        );

        AngerIntent = new IntentionInfo(
            IntentionType.ATTACK,
            AngerDamageAmount.ToString(),
            () => { ActionLib.DamageAction(Player, this, AngerDamageAmount); }
        );
    }

    public override void OnBattleStart()
    {
        
    }

    public override void EnemyChooseIntention(int turnCount)
    {
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
    [SerializeField] int SleepBuffAmount = 6;
    [SerializeField] int RestHealAmount = 6;
    [SerializeField] int AngerDamageAmount = 8;

    IntentionInfo SnoreIntent;

    IntentionInfo RestIntent;

    IntentionInfo AngerIntent;
}
