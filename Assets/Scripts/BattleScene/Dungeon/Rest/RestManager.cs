using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestManager : MonoBehaviour
{
    /// <summary>
    /// 当前的休息节点
    /// </summary>
    public RestNode currNode;

    List<DungeonRoads> roads = new List<DungeonRoads>();

    /// <summary>
    /// 设置休息节点
    /// </summary>
    /// <param name="node">节点</param>
    public void SetRest(RestNode node)
    {
        ClearRestScreen();

        roads = GetComponentsInChildren<DungeonRoads>().ToList();

        for(int i = 0; i < roads.Count; i++)
        {
            if (node.nextNodes is null) print("nextNodes is null");
            roads[i].SetUpRoad(node.nextNodes[i]);
        }
    }

    /// <summary>
    /// 进入撤离战斗
    /// </summary>
    public void GoToEvacuateBattle()
    {
        DungeonNode evacuateBattle = new DungeonNode();
        evacuateBattle.nodeInfo = (currNode.nodeInfo as RestNodeInfo).evacuateBattle;

        DungeonManager.Instance.EnterNode(evacuateBattle);
    }

    /// <summary>
    /// 清除所有被绘制的地图节点
    /// </summary>
    void ClearRestScreen()
    {
        foreach (DungeonRoads road in roads)
        {
            road.ClearNodes();
        }
    }
}
