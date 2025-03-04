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
}
