using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffOwner))]
[RequireComponent(typeof(TakeDamage))]
public abstract class CreatureBehaviour : MonoBehaviour
{
    /// <summary>
    /// 身份识别
    /// </summary>
    public string ID;

    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHealth;

    /// <summary>
    /// 生命处理组件的引用
    /// </summary>
    public TakeDamage takeDamage;

    /// <summary>
    /// buff处理组件的引用
    /// </summary>
    public BuffOwner buffOwner;
    
    /// <summary>
    /// 死亡时触发的回调
    /// </summary>
    public abstract void OnDead();

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    public abstract void OnBattleStart();

    protected virtual void Awake()
    {
        takeDamage = GetComponent<TakeDamage>();
        buffOwner = GetComponent<BuffOwner>();

        takeDamage.ActOnDead += OnDead;
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, OnBattleStart);
    }
}
