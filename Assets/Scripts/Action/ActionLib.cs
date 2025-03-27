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
        Debug.Log("Health of " + target + " now is " + target.takeDamage.Health);
    }

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="target">要受到伤害的目标</param>
    /// <param name="source">攻击的来源</param>
    /// <param name="damage">伤害数值</param>
    public static void DamageAction(CreatureBehaviour target, CreatureBehaviour source, int damage)
    {
        target.takeDamage.GetDamage(damage);
        AnimationManager.Instance.PlayAnimEffect(target.transform.position, "beat");
        Debug.Log("Deal " + damage + " damage to " + target.name);
    }

    /// <summary>
    /// 受到治疗
    /// </summary>
    /// <param name="target">要受到治疗的目标</param>
    /// <param name="source">治疗的来源</param>
    /// <param name="heal">治疗数值</param>
    public static void HealAction(CreatureBehaviour target, CreatureBehaviour source, int heal)
    {
        target.takeDamage.Health += heal;
        Debug.Log("Heal" + heal);
    }

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

    #endregion
}
