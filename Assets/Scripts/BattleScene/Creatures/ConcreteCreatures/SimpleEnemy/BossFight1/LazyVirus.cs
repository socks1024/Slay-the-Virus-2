using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyVirus : EnemyBehaviour
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

        LazyIntent = new IntentionInfo(
            IntentionType.UNKNOWN,
            "",
            null
        );

        YawnIntent = new IntentionInfo(
            IntentionType.GIVE_TRASH,
            YawnCardAmount.ToString(),
            () => { ActionLib.AddVirusCardToDrawPile("Trap", YawnCardAmount); }
        );

        AngerIntent = new IntentionInfo(
            IntentionType.ATTACK,
            new DamageInfo(Player, this, AngerDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, AngerDamageAmount); }
        );

        #endregion

        if (takeDamage.Health >= MaxHealth / 2)
        {
            if (Random.value < YawnRatio)
            {
                SetIntention(YawnIntent);
            }
            else
            {
                SetIntention(LazyIntent);
            }
        }
        else
        {
            if (Random.value < YawnRatio)
            {
                SetIntention(YawnIntent);
            }
            else
            {
                SetIntention(AngerIntent);
            }
        }
    }

    [Header("意图相关数据")]
    [SerializeField] int YawnCardAmount = 1;
    [SerializeField] int AngerDamageAmount = 4;
    [SerializeField] [Range(0,1)] float YawnRatio = 0.25f;

    IntentionInfo LazyIntent;

    IntentionInfo YawnIntent;

    IntentionInfo AngerIntent;
}
