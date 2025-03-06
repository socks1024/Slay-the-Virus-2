using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffOwner))]
[RequireComponent(typeof(TakeDamage))]
public abstract class CreatureBehaviour<T> : MonoBehaviour where T : CreatureData
{
    /// <summary>
    /// 生物数据引用
    /// </summary>
    public T creatureData;

    /// <summary>
    /// 身份识别
    /// </summary>
    public string ID{ get{ return creatureData.ID; } }

    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHealth{ get{ return creatureData.MaxHealth;}}

    /// <summary>
    /// 死亡时触发的回调
    /// </summary>
    public abstract void OnDead();

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    public abstract void OnBattleStart();

    protected virtual void Start()
    {
        GetComponent<TakeDamage>().ActOnDead += OnDead;
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, OnBattleStart);
    }
}
