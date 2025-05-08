using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        takeDamage.OnHealthChange += OnLoseHalfHealth;
        DialogueManager.Instance.StartDialogue("BossFight1_1");
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
        else if (!loseHalfHealth)
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

    bool loseHalfHealth = false;

    #region Dialogue

    void OnLoseHalfHealth(int health, int block)
    {
        if (!loseHalfHealth && health < MaxHealth / 2)
        {
            DialogueManager.Instance.StartDialogue("BossFight1_2");
            loseHalfHealth = true;
        }
    }

    #endregion
}
