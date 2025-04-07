using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionLib
{
    /// <summary>
    /// 直接变更生命值
    /// </summary>
    /// <param name="target">要变更生命值的目标</param>
    /// <param name="amount">变更量</param>
    public static void DirectlyChangeHealthAction(CreatureBehaviour target, int amount)
    {
        target.takeDamage.Health += amount;
    }

    #region battle related actions

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="target">要受到伤害的目标</param>
    /// <param name="source">攻击的来源</param>
    /// <param name="damage">伤害数值</param>
    public static void DamageAction(CreatureBehaviour target, CreatureBehaviour source, int damage)
    {
        AnimationManager.Instance.PlayAnimEffect(target.transform.position, "beat", () => {
            target.takeDamage.GetDamage(damage);
        });
    }

    /// <summary>
    /// 受到治疗
    /// </summary>
    /// <param name="target">要受到治疗的目标</param>
    /// <param name="source">治疗的来源</param>
    /// <param name="heal">治疗数值</param>
    public static void HealAction(CreatureBehaviour target, CreatureBehaviour source, int heal)
    {
        // 治疗动画
        AnimationManager.Instance.PlayAnimEffect(target.transform.position, "beat", () => {
            target.takeDamage.Health += heal;
        });
    }

    /// <summary>
    /// 给予特定Buff
    /// </summary>
    /// <param name="target">要获得buff目标</param>
    /// <param name="source">给予buff的来源</param>
    /// <param name="buffName">buff的名字</param>
    /// <param name="amount">buff的层数</param>
    public static void ApplyBuffAction(CreatureBehaviour target, CreatureBehaviour source, string buffName, int amount)
    {
        // 给予负面BUFF动画
        // 获得正面BUFF动画

        AnimationManager.Instance.PlayAnimEffect(target.transform.position, "beat", () => {
            target.buffOwner.GainBuff(DungeonBuffLib.GetBuff(buffName, amount));
        });
        
    }

    /// <summary>
    /// 获得格挡
    /// </summary>
    /// <param name="target">获得格挡的对象</param>
    /// <param name="source">获得格挡的来源</param>
    /// <param name="amount">格挡的数量</param>
    public static void GainBlockAction(CreatureBehaviour target, CreatureBehaviour source, int amount)
    {
        // 获得格挡动画

        AnimationManager.Instance.PlayAnimEffect(target.transform.position, "beat", () => {
            target.takeDamage.Block += amount;
        });
        
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="amount">抽牌量</param>
    public static void DrawCardAction(int amount)
    {
        DungeonManager.Instance.battleManager.cardFlow.DrawCards(amount);
    }

    /// <summary>
    /// 消耗卡牌
    /// </summary>
    /// <param name="card">要消耗的卡牌</param>
    public static void ExhaustCardAction(CardBehaviour card)
    {
        DungeonManager.Instance.battleManager.cardFlow.ExhaustCard(card);
    }

    /// <summary>
    /// 攻击全体敌人
    /// </summary>
    /// <param name="source">伤害来源</param>
    /// <param name="amount">伤害量</param>
    public static void DamageAllAction(CreatureBehaviour source, int amount)
    {
        foreach (EnemyBehaviour enemy in DungeonManager.Instance.battleManager.enemyGroup.enemies)
        {
            DamageAction(enemy, source, amount);
        }
    }


    #endregion

    #region Dungeon Related Actions

    /// <summary>
    /// 改变玩家的金钱
    /// </summary>
    /// <param name="variation">变化量</param>
    public static void PlayerChangeMoney(int variation)
    {
        DungeonManager.Instance.Player.Nutrition += variation;
    }

    /// <summary>
    /// 给予玩家新的遗物
    /// </summary>
    /// <param name="p_Relic">遗物预制体</param>
    public static void PlayerGainRelic(RelicBehaviour p_Relic)
    {
        DungeonManager.Instance.Player.p_Relics.Add(p_Relic);
    }

    /// <summary>
    /// 给予玩家新的卡牌并添加至卡组
    /// </summary>
    /// <param name="card">卡牌预制体</param>
    public static void PlayerAddCardToDeck(CardBehaviour card)
    {
        DungeonManager.Instance.Player.p_Deck.Add(card);
    }

    #endregion
}
