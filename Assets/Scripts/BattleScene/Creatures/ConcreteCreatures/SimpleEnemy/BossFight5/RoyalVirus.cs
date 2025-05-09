using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoyalVirus : EnemyBehaviour
{
    public override void ActOnEnterBattle()
    {
        DialogueManager.Instance.StartDialogue("BossFight5_1");
    }

    public override void EnemyChooseIntention(int turnCount)
    {
        #region generate intent

        PierceIntent = new IntentionInfo(
            IntentionType.TRIPLE_ATTACK,
            new DamageInfo(Player, this, PierceDamageAmount).finalDamage.ToString(),
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
            () => { ActionLib.HealAction(this, this, DrinkHealAmount);
                ActionLib.ApplyBuffAction(this, this, "Strength", DrinkStrengthAmount);}
        );

        NobleIntent = new IntentionInfo(
            IntentionType.GIVE_TRASH,
            NobleTrashAmount.ToString(),
            () => { ActionLib.AddVirusCardToDrawPile("Spy", NobleTrashAmount);}
        );

        #endregion

        if (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyByID("Waiter") is null)
        {
            if (turnCount % 2 == 1)
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
            if (DungeonManager.Instance.battleManager.enemyGroup.GetEnemyByID("NobleVirus1") != null || DungeonManager.Instance.battleManager.enemyGroup.GetEnemyByID("NobleVirus2") != null)
            {
                if (turnCount % 2 == 1)
                {
                    SetIntention(DrinkIntent);
                }
                else
                {
                    SetIntention(NobleIntent);
                }
            }
            else
            {
                if (turnCount % 2 == 1)
                {
                    SetIntention(DrinkIntent);
                }
                else
                {
                    if (Random.value < 0.5)
                    {
                        SetIntention(PierceIntent);
                    }
                    else
                    {
                        SetIntention(SlashIntent);
                    }
                }
            }
        }
    }

    public override void OnBattleStart()
    {
        
    }

    [Header("意图相关数据")]
    [SerializeField] int PierceDamageAmount = 3;
    [SerializeField] int SlashDamageAmount = 9;
    [SerializeField] int SlashWoundAmount = 3;
    [SerializeField] int DrinkHealAmount = 6;
    [SerializeField] int DrinkStrengthAmount = 1;
    [SerializeField] int NobleTrashAmount = 2;

    IntentionInfo PierceIntent;

    IntentionInfo SlashIntent;

    IntentionInfo DrinkIntent;

    IntentionInfo NobleIntent;

    public Sprite angrySprite;

    public void GetAngry()
    {
        DialogueManager.Instance.StartDialogue("BossFight5_2");
        GetComponent<Image>().sprite = angrySprite;
    }
}
