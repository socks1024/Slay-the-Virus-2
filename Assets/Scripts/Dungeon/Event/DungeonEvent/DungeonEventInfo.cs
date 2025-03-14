using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有副本事件的基类
/// </summary>
public abstract class DungeonEventInfo : ScriptableObject
{
    
}

/// <summary>
/// 遭遇战事件
/// </summary>
public abstract class BattleInfo : DungeonEventInfo
{
    /// <summary>
    /// 遭遇战中的敌人预制体，按顺序排列
    /// </summary>
    public List<GameObject> enemies;

    /// <summary>
    /// 战利品信息
    /// </summary>
    public LootInfo lootInfo;

    /// <summary>
    /// 当前遭遇战中每回合结束时触发的回调（例如补充敌人）
    /// </summary>
    /// <param name="turnCount">回合数</param>
    public abstract void OnAllActEndCallback(int turnCount);
}

/// <summary>
/// 带有描述和选项的事件
/// </summary>
public abstract class NormalEventInfo : DungeonEventInfo
{
    /// <summary>
    /// 事件的文本描述
    /// </summary>
    public string text;

    /// <summary>
    /// 事件的选项
    /// </summary>
    public List<EventChoice> choices;
}

// /// <summary>
// /// 撤离点事件（选择是否撤退）
// /// </summary>
// public abstract class EvacuateInfo : NormalEventInfo
// {
//     public void Evacuate()
//     {
//         Debug.Log("Evacuate");
//     }
// }

// /// <summary>
// /// 岔路口事件，用于衔接数个事件并选择
// /// </summary>
// public abstract class ForkRoadInfo : NormalEventInfo
// {
//     /// <summary>
//     /// 通向的后续几个事件
//     /// </summary>
//     public List<DungeonEventInfo> eventInfos;
// }
