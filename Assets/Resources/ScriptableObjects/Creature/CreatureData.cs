using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureData : ScriptableObject
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
    /// 生物名称
    /// </summary>
    public string Name;
}
