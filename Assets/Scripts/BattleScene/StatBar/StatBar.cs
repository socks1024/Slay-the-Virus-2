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
    /// 显示格挡值的背景
    /// </summary>
    public Image shieldBG;

    /// <summary>
    /// 显示格挡值的文字
    /// </summary>
    TextMeshProUGUI shieldText;

    [SerializeField] ShowHealthText showHealthText;

    void Awake()
    {
        //healthBar = GetComponentInChildren<Slider>();
        //buffsRoot = transform.Find("BuffList");

        showHealthText.takeDamage = healthOwner;

        shieldText = shield.GetComponentInChildren<TextMeshProUGUI>();

        healthBar.maxValue = healthOwner.MaxHealth;
        healthBar.value = healthOwner.MaxHealth;
        
        healthOwner.OnHealthChange += UpdateHealth;
        buffOwner.OnChangeBuff += UpdateBuffs;

        shieldText.text = "";
        shield.gameObject.SetActive(false);
        shieldBG.gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新生命条
    /// </summary>
    void UpdateHealth(int health, int block)
    {
        healthBar.value = health;
        healthBar.maxValue = healthOwner.MaxHealth;

        if (block > 0)
        {
            shield.gameObject.SetActive(true);
            shieldText.text = block.ToString();
        }
        else
        {
            shieldText.text = "";
            shield.gameObject.SetActive(false);
            shieldBG.gameObject.SetActive(false);
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

    void OnDestroy()
    {
        healthOwner.OnHealthChange -= UpdateHealth;
        buffOwner.OnChangeBuff -= UpdateBuffs;
    }


}
