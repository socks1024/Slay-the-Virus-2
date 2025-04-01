using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Slider healthBar;

    /// <summary>
    /// 显示的所有buff的父物体
    /// </summary>
    public Transform buffsRoot;

    /// <summary>
    /// 显示格挡值的图标
    /// </summary>
    public Image shield;

    /// <summary>
    /// 显示格挡值的文字
    /// </summary>
    TextMeshProUGUI shieldText;

    void Start()
    {
        //healthBar = GetComponentInChildren<Slider>();
        //buffsRoot = transform.Find("BuffList");

        shieldText = shield.GetComponentInChildren<TextMeshProUGUI>();

        healthBar.maxValue = healthOwner.MaxHealth;
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

        if (block > 0)
        {
            shield.gameObject.SetActive(true);
            shieldText.text = block.ToString();
        }
        else
        {
            shield.gameObject.SetActive(false);
            shieldText.text = "";
        }
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
