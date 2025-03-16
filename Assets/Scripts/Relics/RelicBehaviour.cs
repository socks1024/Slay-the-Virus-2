using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelicBehaviour : MonoBehaviour
{
    // /// <summary>
    // /// 道具的数据
    // /// </summary>
    // public ItemData item;

    /// <summary>
    /// 道具的ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 检视道具时显示的名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 检视道具时显示的描述
    /// </summary>
    public string Description;

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    public abstract void ActOnBattleStart();

    /// <summary>
    /// 回合开始时触发的回调
    /// </summary>
    public abstract void ActOnTurnStart();

    protected virtual void Awake()
    {
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, ActOnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ActOnTurnStart);
    }
}
