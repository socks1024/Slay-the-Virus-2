using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        DialogueManager.Instance.StartDialogue("BossFight2_1");
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        DashIntent = new IntentionInfo(
            IntentionType.ATTACK_AND_GIVE_DEBUFF,
            new DamageInfo(Player, this, DashDamageAmount).finalDamage.ToString(),
            () => { ActionLib.DamageAction(Player, this, DashDamageAmount);
                ActionLib.ApplyBuffNextTurnAction(Player, this, "Wound", DashBuffAmount);}
        );

        ScrapeIntent = new IntentionInfo(
            IntentionType.GIVE_DEBUFF,
            ScrapeBuffAmount.ToString(),
            () => { ActionLib.ApplyBuffNextTurnAction(Player, this, "Wound", ScrapeBuffAmount);}
        );

        TentacleIntent = new IntentionInfo(
            IntentionType.GAIN_BUFF_AND_GIVE_DEBUFF,
            TentacleStrengthAmount.ToString(),
            () => { //ActionLib.ApplyBuffNextTurnAction(Player, this, "Weakness", TentacleWeaknessAmount);
                ActionLib.ApplyBuffAction(this, this, "Strength", TentacleStrengthAmount);}
        );

        #endregion

        if (turnCount % 3 == 1)
        {
            SetIntention(DashIntent);
        }
        else if (turnCount % 3 == 2)
        {
            SetIntention(ScrapeIntent);
        }
        else if (turnCount % 3 == 0)
        {
            SetIntention(TentacleIntent);
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int DashDamageAmount = 8;
    [SerializeField] int DashBuffAmount = 2;
    [SerializeField] int ScrapeBuffAmount = 5;
    // [SerializeField] int TentacleWeaknessAmount = 1;
    [SerializeField] int TentacleStrengthAmount = 1;

    IntentionInfo DashIntent;

    IntentionInfo ScrapeIntent;

    IntentionInfo TentacleIntent;
}
