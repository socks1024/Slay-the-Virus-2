using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionLib
{
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
}
