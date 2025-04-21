using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobleVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        PierceIntent = new IntentionInfo(
            IntentionType.TRIPLE_ATTACK,
            new DamageInfo(Player, this, PierceDamageAmount).finalDamage.ToString() + "×3",
            () => { ActionLib.MultiAttackAction(Player, this, PierceDamageAmount, 3);}
        );

        SlashIntent = new IntentionInfo(
            IntentionType.ATTACK_AND_GIVE_DEBUFF,
            new DamageInfo(Player, this, SlashDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, SlashDamageAmount);
                ActionLib.ApplyBuffNextTurnAction(Player, this, "Wound", SlashWoundAmount);}
        );

        DrinkIntent = new IntentionInfo(
            IntentionType.HEAL,
            DrinkHealAmount.ToString(),
            () => { ActionLib.HealAction(this, this, DrinkHealAmount);}
        );

        #endregion

        if (turnCount % 2 == AttackTurn)
        {
            if (Random.value > 0.5f)
            {
                SetIntention(PierceIntent);
            }
            else
            {
                SetIntention(SlashIntent);
            }
        }
        else
        {
            SetIntention(DrinkIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    public int AttackTurn;

    [Header("意图相关数据")]
    [SerializeField] int PierceDamageAmount = 3;
    [SerializeField] int SlashDamageAmount = 9;
    [SerializeField] int SlashWoundAmount = 3;
    [SerializeField] int DrinkHealAmount = 6;

    IntentionInfo PierceIntent;

    IntentionInfo SlashIntent;

    IntentionInfo DrinkIntent;
}
