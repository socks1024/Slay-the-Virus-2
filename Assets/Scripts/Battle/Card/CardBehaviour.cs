using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBehaviour : MonoBehaviour
{
    /// <summary>
    /// 卡牌初始化
    /// </summary>
    protected virtual void Initialize(){}

    /// <summary>
    /// 卡牌的回合结束时效果
    /// </summary>
    public virtual void ActOnTurnEnd(){}

    /// <summary>
    /// 卡牌的填充时效果
    /// </summary>
    public virtual void ActOnPlaced(){}

    /// <summary>
    /// 卡牌的取下时效果
    /// </summary>
    public virtual void ActOnRemoved(){}

    /// <summary>
    /// 卡牌的被丢弃时效果
    /// </summary>
    public virtual void ActOnDiscard(){}
}
