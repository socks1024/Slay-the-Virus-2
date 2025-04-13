using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBehaviour : MonoBehaviour
{
    #region Buff Data

    /// <summary>
    /// 身份识别
    /// </summary>
    public string ID;

    /// <summary>
    /// Buff的名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 状态效果的图片
    /// </summary>
    public Sprite BuffSprite;

    /// <summary>
    /// 状态效果的类型
    /// </summary>
    public BuffType Type;

    /// <summary>
    /// 状态效果的持有量
    /// </summary>
    public int Amount
    { 
        get { return amount; }
        set 
        {
            amount = value;
            if (amount == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    int amount;

    /// <summary>
    /// 状态效果的持有者
    /// </summary>
    public CreatureBehaviour Owner{ get; set;}

    #endregion

    #region Buff Acts

    /// <summary>
    /// Buff的回合结束时效果
    /// </summary>
    public virtual void ActOnTurnEnd()
    {

    }

    /// <summary>
    /// Buff的回合开始时效果
    /// </summary>
    public virtual void ActOnTurnStart()
    {

    }

    #endregion

    protected virtual void Start()
    {
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, ActOnTurnEnd);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ActOnTurnStart);
    }

    protected virtual void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.ENEMY_ACT_END, ActOnTurnEnd);
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, ActOnTurnStart);
    }
}

public enum BuffType
{
    POSITIVE,
    NEGATIVE,
}


