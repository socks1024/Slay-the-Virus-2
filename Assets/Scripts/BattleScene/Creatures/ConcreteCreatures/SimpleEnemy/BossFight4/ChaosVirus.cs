using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        DialogueManager.Instance.StartDialogue("BossFight4_1");
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        StrikeIntent = new IntentionInfo(
            IntentionType.ATTACK,
            new DamageInfo(Player, this, StrikeDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, StrikeDamageAmount);}
        );

        RoarIntent = new IntentionInfo(
            IntentionType.GAIN_BUFF_AND_GIVE_DEBUFF,
            RoarWeaknessAmount.ToString(),
            () => { ActionLib.ApplyBuffNextTurnAction(Player, this, "Weakness", RoarWeaknessAmount);
                ActionLib.ApplyBuffAction(this, this, "Strength", RoarStrengthAmount);}
        );

        SplitIntent = new IntentionInfo(
            IntentionType.GIVE_TRASH,
            SplitTrashAmount.ToString(),
            () => { ActionLib.AddVirusCardToDrawPile("Assassin", SplitTrashAmount);}
        );

        HealIntent = new IntentionInfo(
            IntentionType.HEAL,
            HealHealAmount.ToString(),
            () => { ActionLib.HealAction(this, this, HealHealAmount);}
        );

        GazeIntent = new IntentionInfo(
            IntentionType.GIVE_DEBUFF,
            GazeWoundAmount.ToString(),
            () => { ActionLib.ApplyBuffNextTurnAction(Player, this, "Wound", GazeWoundAmount);
                ActionLib.ApplyBuffNextTurnAction(Player, this, "Paralyze", GazeParalyzeAmount);}
        );

        SwellIntent = new IntentionInfo(
            IntentionType.DEACTIVATE_SQUARE,
            "",
            () => { ActionLib.DisableRandomSquareAction(SwellSquareAmount);}
        );

        #endregion

        int r = Random.Range(0,6);

        if ( r == 0 )
        {
            SetIntention(StrikeIntent);
        }
        else if ( r == 1 )
        {
            SetIntention(RoarIntent);
        }
        else if ( r == 2 )
        {
            SetIntention(SplitIntent);
        }
        else if ( r == 3 )
        {
            SetIntention(GazeIntent);
        }
        else if ( r == 4 )
        {
            SetIntention(HealIntent);
        }
        else if ( r == 5 )
        {
            SetIntention(SwellIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int StrikeDamageAmount = 12;
    [SerializeField] int RoarWeaknessAmount = 3;
    [SerializeField] int RoarStrengthAmount = 3;
    [SerializeField] int SplitTrashAmount = 2;
    [SerializeField] int GazeWoundAmount = 6;
    [SerializeField] int GazeParalyzeAmount = 4;
    [SerializeField] int HealHealAmount = 12;
    [SerializeField] int SwellSquareAmount = 1;


    IntentionInfo StrikeIntent;

    IntentionInfo RoarIntent;

    IntentionInfo SplitIntent;

    IntentionInfo GazeIntent;

    IntentionInfo HealIntent;

    IntentionInfo SwellIntent;
}
