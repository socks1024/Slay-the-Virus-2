using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        return node;
    }

    /// <summary>
    /// 初始化时获取所有地图节点
    /// </summary>
    [InitializeOnLoadMethod]
    public static void InitGetNodes()
    {
        LoadBattleNode("TestBattle");
        LoadEventNode("TestEvent");
        LoadEventNode("TestEvacuateEvent");
    }

    /// <summary>
    /// 将一个地图节点加载入总存储
    /// </summary>
    /// <param name="relativeAdress">相对于DungeonNodes文件夹的地址</param>
    static void LoadDungeonNode(string relativeAdress)
    {
        DungeonNodeInfo dungeonNode = Resources.Load<DungeonNodeInfo>("ScriptableObjects/DungeonNodes/" + relativeAdress);
        dungeonNodes.Add(dungeonNode.nodeID, dungeonNode);
    }

    /// <summary>
    /// 将一个战斗节点加入总存储
    /// </summary>
    /// <param name="name">节点的名字</param>
    static void LoadBattleNode(string name)
    {
        LoadDungeonNode("Battle/" + name);
    }

    /// <summary>
    /// 将一个事件节点加入总存储
    /// </summary>
    /// <param name="name">节点的名字</param>
    static void LoadEventNode(string name)
    {
        LoadDungeonNode("Event/" + name);
    }
}
