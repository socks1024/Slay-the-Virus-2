using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CardAct
{
    /// <summary>
    /// 卡牌的回合结束时效果
    /// </summary>
    public void ActOnTurnEnd();

    /// <summary>
    /// 卡牌的填充时效果
    /// </summary>
    public void ActOnPlaced();

    /// <summary>
    /// 卡牌的取下时效果
    /// </summary>
    public void ActOnRemoved();

    /// <summary>
    /// 卡牌的被丢弃时效果
    /// </summary>
    public void ActOnDiscard();
}

//DamageAction(source,target,damage)
