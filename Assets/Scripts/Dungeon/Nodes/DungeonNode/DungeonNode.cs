using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 地牢节点
/// </summary>
public abstract class DungeonNode
{
    public string nodeID;

    public List<DungeonNode> connectedNodes;

    public bool visited = false;
}

/// <summary>
/// 战斗节点
/// </summary>
public class BattleNode : DungeonNode
{
    /// <summary>
    /// 敌人预制体列表
    /// </summary>
    public List<EnemyBehaviour> p_Enemies;

    /// <summary>
    /// 每回合结束时回调
    /// </summary>
    public UnityAction<int> OnAllActEndCallback;
}

/// <summary>
/// 事件节点
/// </summary>
public class EventNode : DungeonNode
{
    /// <summary>
    /// 事件描述
    /// </summary>
    public string eventDescription;

    /// <summary>
    /// 事件选项
    /// </summary>
    public List<EventChoice> choices;
}

/// <summary>
/// 撤离节点
/// </summary>
public class EvacuateNode : EventNode
{
    /// <summary>
    /// 设置选项
    /// </summary>
    public void SetUpRoads()
    {
        choices.Add(new EventChoice("撤离", () => { Debug.Log("Evacuate"); }));

        foreach (DungeonNode node in connectedNodes)
        {
            choices.Add(new EventChoice("前往" + node.nodeID, () => nextNode = node ));
        }
    }

    /// <summary>
    /// 被选择的节点
    /// </summary>
    public DungeonNode nextNode;
}