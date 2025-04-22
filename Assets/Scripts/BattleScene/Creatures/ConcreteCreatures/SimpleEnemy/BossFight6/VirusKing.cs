using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusKing : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        takeDamage.lockHealthAtOne = true;
    }

    int nextIntentNumber = 0;

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        string virusID = "Trap";

        float r = Random.value;
        if (r < 1/4)
        {
            virusID = "RoadExplosion";
        }
        else if (r < 2/4)
        {
            virusID = "Barrier";
        }
        else if (r < 3/4)
        {
            virusID = "Spy";
        }

        ScornIntent = new IntentionInfo(
            IntentionType.GIVE_TRASH,
            ScornTrashAmount.ToString(),
            () => {ActionLib.AddVirusCardToDrawPile(virusID, ScornTrashAmount);}
        );

        SlimeIntent = new IntentionInfo(
            IntentionType.GIVE_DEBUFF,
            SlimeParalyzeAmount.ToString(),
            () => {ActionLib.ApplyBuffNextTurnAction(Player, this, "Paralyze", SlimeParalyzeAmount);}
        );

        BashIntent = new IntentionInfo(
            IntentionType.ATTACK_AND_GIVE_DEBUFF,
            new DamageInfo(Player, this, BashDamageAmount).finalDamage.ToString(),
            () => {ActionLib.DamageAction(Player, this, BashDamageAmount);
                ActionLib.ApplyBuffNextTurnAction(Player, this, "Wound", BashWoundAmount);}
        );

        SuppressIntent = new IntentionInfo(
            IntentionType.DEACTIVATE_SQUARE,
            SuppressLockAmount.ToString(),
            () => {ActionLib.DisableRandomSquareAction(SuppressLockAmount);}
        );

        GrowthIntent = new IntentionInfo(
            IntentionType.GAIN_BUFF,
            GrowthStrengthAmount.ToString(),
            () => {ActionLib.ApplyBuffAction(this, this, "Strength", GrowthStrengthAmount);}
        );

        DoubleAttackIntent = new IntentionInfo(
            IntentionType.DOUBLE_ATTACK,
            new DamageInfo(Player, this, DoubleAttackDamageAmount).finalDamage.ToString() + "×2",
            () => {ActionLib.MultiAttackAction(Player, this, DoubleAttackDamageAmount, 2);}
        );

        DyingIntent = new IntentionInfo(
            IntentionType.UNKNOWN,
            "",
            () => {
                EnemyBehaviour virusFollower = DungeonManager.Instance.battleManager.enemyGroup.GetEnemyByID("VirusFollower");
                if (virusFollower != null)
                {
                    ActionLib.HealAction(this, this, DyingHealAmount);
                    ActionLib.DamageAction(virusFollower, this, 999);
                }
                else
                {
                    takeDamage.lockHealthAtOne = false;
                    ActionLib.DamageAction(this, this, 999);
                }
            }
        );

        OrderIntent = new IntentionInfo(
            IntentionType.SUMMON,
            "",
            () => {ActionLib.SummonEnemyAction("VirusFollower");}
        );

        #endregion

        if (takeDamage.Health <= 1)
        {
            SetIntention(DyingIntent);
        }
        else
        {
            switch (nextIntentNumber)
            {
                case 0:
                    SetIntention(ScornIntent);
                    nextIntentNumber ++;
                    break;
                case 1:
                    SetIntention(SlimeIntent);
                    nextIntentNumber ++;
                    break;
                case 2:
                    SetIntention(BashIntent);
                    if (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyCount() < 5)
                    {
                        nextIntentNumber ++;
                    }
                    else
                    {
                        nextIntentNumber += 2;
                    }
                    break;
                case 3:
                    SetIntention(OrderIntent);
                    nextIntentNumber ++;
                    break;
                case 4:
                    SetIntention(SuppressIntent);
                    nextIntentNumber ++;
                    break;
                case 5:
                    SetIntention(GrowthIntent);
                    nextIntentNumber ++;
                    break;
                case 6:
                    SetIntention(DoubleAttackIntent);
                    if (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyCount() < 5)
                    {
                        nextIntentNumber ++;
                    }
                    else
                    {
                        nextIntentNumber = 0;
                    }
                    break;
                case 7:
                    SetIntention(OrderIntent);
                    nextIntentNumber = 0;
                    break;
            }
        }
    }

    public override void OnBattleStart()
    {
        
    }

    

    IntentionInfo ScornIntent;
    [Header("意图相关数据")]
    [SerializeField] int ScornTrashAmount = 2;

    IntentionInfo SlimeIntent;
    [SerializeField] int SlimeParalyzeAmount = 2;

    IntentionInfo BashIntent;
    [SerializeField] int BashDamageAmount = 10;
    [SerializeField] int BashWoundAmount = 4;

    IntentionInfo SuppressIntent;
    [SerializeField] int SuppressLockAmount = 1;

    IntentionInfo GrowthIntent;
    [SerializeField] int GrowthStrengthAmount = 2;

    IntentionInfo DoubleAttackIntent;
    [SerializeField] int DoubleAttackDamageAmount = 7;

    IntentionInfo DyingIntent;
    [SerializeField] int DyingHealAmount = 25;

    IntentionInfo OrderIntent;

}
