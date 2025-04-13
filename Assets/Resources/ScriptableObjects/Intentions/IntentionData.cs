using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObject/IntentionData", order = 0)]
public class IntentionData : ScriptableObject
{
    /// <summary>
    /// 意图的ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 意图的类型
    /// </summary>
    public IntentionType intentionType;

    /// <summary>
    /// 意图目标的类型
    /// </summary>
    public TargetType targetType;

    /// <summary>
    /// 意图的描述
    /// </summary>
    public string Description;
}


