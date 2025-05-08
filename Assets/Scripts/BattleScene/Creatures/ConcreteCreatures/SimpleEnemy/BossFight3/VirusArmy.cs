using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusArmy : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        DialogueManager.Instance.StartDialogue("BossFight3_1");
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        int allAttackDamage = AllAttackDamageAmount + AllAttackDamageIncreasement * (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyCount() - 1);

        AllAttackIntent = new IntentionInfo(
            IntentionType.ATTACK,
            new DamageInfo(Player, this, allAttackDamage).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, allAttackDamage);}
        );

        string summonID = "EvilVirus";

        if (Random.value > 0.5f)
        {
            summonID = "HardVirus";
        }

        SummonIntent = new IntentionInfo(
            IntentionType.SUMMON,
            "",
            () => { ActionLib.SummonEnemyAction(summonID);}
        );

        #endregion

        if (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyCount() >= 5)
        {
            SetIntention(AllAttackIntent);
        }
        else
        {
            if (turnCount % 2 == 1)
            {
                SetIntention(AllAttackIntent);
            }
            else if (turnCount % 2 == 0)
            {
                SetIntention(SummonIntent);
            }
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int AllAttackDamageAmount = 3;
    [SerializeField] int AllAttackDamageIncreasement = 3;

    IntentionInfo AllAttackIntent;

    IntentionInfo SummonIntent;
}
