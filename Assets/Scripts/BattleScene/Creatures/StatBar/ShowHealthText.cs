using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHealthText : MonoBehaviour
{
    /// <summary>
    /// 生命文本关联的生命条
    /// </summary>
    public TakeDamage takeDamage;

    TextMeshProUGUI tmp;

    void Start()
    {
        takeDamage.OnHealthChange += OnHealthChange;
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = takeDamage.Health + "/" + takeDamage.MaxHealth;
    }

    /// <summary>
    /// 生命值变化时触发的回调
    /// </summary>
    /// <param name="health">当前生命值</param>
    /// <param name="block">当前格挡值</param>
    void OnHealthChange(int health, int block)
    {
        tmp.text = health + "/" + takeDamage.MaxHealth;
    }
}
