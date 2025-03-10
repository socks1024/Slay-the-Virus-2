using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    /// <summary>
    /// 控制生命值的组件
    /// </summary>
    public TakeDamage healthOwner;

    /// <summary>
    /// 控制buff的组件
    /// </summary>
    public BuffOwner buffOwner;

    /// <summary>
    /// 生命值条
    /// </summary>
    Slider healthBar;

    /// <summary>
    /// 显示的所有buff的父物体
    /// </summary>
    Transform buffsRoot;

    void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
        buffsRoot = transform.Find("BuffList");

        healthBar.maxValue = healthOwner.Health;
        healthBar.value = healthBar.maxValue;
        
        healthOwner.OnHealthChange += UpdateHealth;
        buffOwner.OnChangeBuff += UpdateBuffs;
    }

    /// <summary>
    /// 更新生命条
    /// </summary>
    void UpdateHealth(int health, int block)
    {
        healthBar.value = health;
    }

    /// <summary>
    /// 更新状态栏
    /// </summary>
    void UpdateBuffs(List<BuffBehaviour> buffs)
    {
        foreach (BuffBehaviour buff in buffs)
        {
            buff.transform.SetParent(buffsRoot, false);
        }
    }


}
