using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonNodeLib
{
    /// <summary>
    /// 节点总存储
    /// </summary>
    public static Dictionary<string,DungeonNodeInfo> dungeonNodes = new Dictionary<string, DungeonNodeInfo>();

    /// <summary>
    /// 获取某个给定的节点
    /// </summary>
    /// <param name="nodeID">节点的ID</param>
    public static DungeonNode GetNode(string nodeID)
    {
        DungeonNode node = new DungeonNode();

        node.nodeInfo = dungeonNodes[nodeID];

        if (node.nodeInfo is BattleNodeInfo) node.nodeType = DungeonNodeType.BATTLE;
        if (node.nodeInfo is EventNodeInfo) node.nodeType = DungeonNodeType.EVENT;
        if (node is RestNode) node.nodeType = DungeonNodeType.REST;

        return node;
    }

    /// <summary>
    /// 获取某个给定的休息/撤离节点
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public static RestNode GetRestNode(string nodeID)
    {
        RestNode node = new RestNode();
        node.nodeInfo = dungeonNodes[nodeID] as RestNodeInfo;
        node.nodeType = DungeonNodeType.REST;
        return node;
    }

    /// <summary>
    /// 初始化时获取所有地图节点
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    public static void InitSetNodes()
    {
        LoadDungeonNodeInfo();
    }

    /// <summary>
    /// 将地图节点加载入总存储
    /// </summary>
    static void LoadDungeonNodeInfo()
    {
        DungeonNodeInfo[] nodes = Resources.LoadAll<DungeonNodeInfo>("ScriptableObjects/DungeonNodes/");
        foreach (DungeonNodeInfo dungeonNode in nodes)
        {
            dungeonNodes.Add(dungeonNode.nodeID, dungeonNode);
        }
        
    }

    // /// <summary>
    // /// 将一个战斗节点加入总存储
    // /// </summary>
    // static void LoadBattleNodeInfo()
    // {
    //     LoadDungeonNodeInfo("Battle");
    // }

    // /// <summary>
    // /// 将一个事件节点加入总存储
    // /// </summary>
    // /// <param name="name">节点的名字</param>
    // static void LoadEventNodeInfo(string name)
    // {
    //     LoadDungeonNodeInfo("Event/" + name);
    // }

    // /// <summary>
    // /// 将一个休息节点加入总存储
    // /// </summary>
    // /// <param name="name">节点的名字</param>
    // static void LoadRestNodeInfo(string name)
    // {
    //     LoadDungeonNodeInfo("Rest/" + name);
    // }
}
