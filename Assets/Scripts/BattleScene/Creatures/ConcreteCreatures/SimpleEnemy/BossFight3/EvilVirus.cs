using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilVirus : EnemyBehaviour
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

        LaughIntent = new IntentionInfo(
            IntentionType.GAIN_BUFF,
            LaughBuffAmount.ToString(),
            () => { ActionLib.ApplyBuffNextTurnAction(this, this, "Strength", LaughBuffAmount);}
        );

        #endregion

        if (Random.value < 0.5f)
        {
            SetIntention(AttackIntent);
        }
        else
        {
            SetIntention(LaughIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int AttackDamageAmount = 4;
    [SerializeField] int LaughBuffAmount = 5;

    IntentionInfo AttackIntent;

    IntentionInfo LaughIntent;
}
