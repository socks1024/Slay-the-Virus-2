using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(IntentionShow))]
public abstract class IntentionBehaviour : MonoBehaviour
{
    /// <summary>
    /// 意图数据
    /// </summary>
    public IntentionData intentionData;

    /// <summary>
    /// 意图的ID
    /// </summary>
    public string ID{ get{return intentionData.ID;}}

    /// <summary>
    /// 意图的类型
    /// </summary>
    public IntentionType intentionType{ get{return intentionData.intentionType;}}

    /// <summary>
    /// 意图目标的类型
    /// </summary>
    public TargetType targetType{ get{return intentionData.targetType;}}

    /// <summary>
    /// 意图的强度
    /// </summary>
    [HideInInspector]public int Amount;

    /// <summary>
    /// 意图的描述
    /// </summary>
    public string Description{ get{return intentionData.Description;}}

    /// <summary>
    /// 意图的目标
    /// </summary>
    [HideInInspector]public CreatureBehaviour target;
    
    /// <summary>
    /// 意图的来源
    /// </summary>
    [HideInInspector]public CreatureBehaviour source;

    /// <summary>
    /// 意图的行为
    /// </summary>
    public abstract void ActOnEnemyTurn();

    protected virtual void Start()
    {
        
    }
}


