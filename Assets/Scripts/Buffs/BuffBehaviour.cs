using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBehaviour : MonoBehaviour
{
    #region Buff Data

    /// <summary>
    /// 状态效果数据的引用
    /// </summary>
    BuffData buffData;

    /// <summary>
    /// 状态效果的ID
    /// </summary>
    public string ID{ get{ return buffData.ID; } }

    /// <summary>
    /// 状态效果的类型
    /// </summary>
    public BuffType buffType{ get{ return buffData.Type;}}

    /// <summary>
    /// 状态效果的持有量
    /// </summary>
    public int Amount{ get; set; }

    /// <summary>
    /// 状态效果的持有者
    /// </summary>
    public CreatureBehaviour<CreatureData> Owner{ get; set;}

    #endregion

    #region Buff Acts

    /// <summary>
    /// Buff的回合结束时效果
    /// </summary>
    public abstract void ActOnTurnEnd();

    #endregion

    protected virtual void Start()
    {
        EventCenter.Instance.AddEventListener(EventType.TURN_END, ActOnTurnEnd);
    }
}


