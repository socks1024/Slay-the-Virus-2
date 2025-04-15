using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public DungeonMapMode MapMode;

    [Header("地图参数")]
    public int layers = 5;
    public int roadsPerLayer = 3;
    public int minNodesPerRoad = 2;
    public int maxNodesPerRoad = 4;

    [Header("节点类型权重")]
    public int battleNodeChance;
    public int awardNodeChance;
    public int challengeNodeChance;

    [HideInInspector]public List<List<List<DungeonNode>>> mapLayers = new List<List<List<DungeonNode>>>();
    [HideInInspector]public List<RestNode> restNodes = new List<RestNode>();
    [HideInInspector]public DungeonNode startNode;
    [HideInInspector]public DungeonNode bossNode;

    /// <summary>
    /// 将mapLayers填充为正式地图
    /// </summary>
    public void GenerateMap()
    {
        mapLayers.Clear();

        if (MapMode == DungeonMapMode.BOSSFIGHT)
        {
            startNode = GetRandomNodeForBoss();
        }
        else if (MapMode == DungeonMapMode.DUNGEON)
        {
            startNode = GetRandomNodeForStart();//添加起始遭遇战节点

            for (int i = 0; i < layers; i++)
            {
                restNodes.Add(GetRandomNodeForRestEvent(i));//添加撤离节点

                //将上一个 Layer 的最后一列节点连接到新撤离节点
                if (i == 0)
                {
                    startNode.connectedNodes.Add(restNodes[0]);
                }
                else
                {
                    mapLayers[i - 1].ForEach(road => road[road.Count-1].connectedNodes.Add(restNodes[i]));
                }

                List<List<DungeonNode>> currentLayer = new List<List<DungeonNode>>();
                int nodesInRoad = Random.Range(minNodesPerRoad, maxNodesPerRoad + 1);
                
                //一共 roadsPerLayer 条岔路
                for (int j = 0; j < roadsPerLayer; j++)
                {
                    List<DungeonNode> road = new List<DungeonNode>();

                    //每条岔路上有 minNodesPerRoad 到 maxNodesPerRoad 个节点
                    for (int k = 0; k < nodesInRoad; k++)
                    {
                        DungeonNode newNode;
                    
                        newNode = GetRandomNodeForRoad(i);
                        
                        road.Add(newNode);

                        //将新节点与前一个节点连接
                        if (k == 0)
                        {
                            restNodes[i].connectedNodes.Add(newNode);
                        }
                        else
                        {
                            road[k-1].connectedNodes.Add(newNode);
                        }
                    }

                    //将该线路加入休息点显示
                    restNodes[i].nextNodes.Add(road);

                    currentLayer.Add(road);
                }
                
                mapLayers.Add(currentLayer);
            }

            bossNode = GetRandomNodeForBoss();//添加BOSS战节点

            //将BOSS战节点连接到上一列节点
            mapLayers[mapLayers.Count - 1].ForEach(road => road[road.Count-1].connectedNodes.Add(bossNode));
        }
    }

    /// <summary>
    /// 抽取中间节点
    /// </summary>
    /// <returns>下一个中间节点</returns>
    public DungeonNode GetRandomNodeForRoad(int mapDepth)
    {
        float r = Random.value;

        float totalWeight = battleNodeChance + awardNodeChance + challengeNodeChance;

        float battleChance = battleNodeChance / totalWeight;
        float awardChance = awardNodeChance / totalWeight;

        
        if (r < battleChance) 
            return GetRandomNodeForEncounter(mapDepth);//添加遭遇战节点
        if (r < battleChance + awardChance)
            return GetRandomNodeForAwardEvent(mapDepth);//添加奖励事件节点
        else
            return GetRandomNodeForChallengeEvent(mapDepth);//添加挑战事件节点
    }

    /// <summary>
    /// 抽取起始节点
    /// </summary>
    /// <returns>起始节点</returns>
    public DungeonNode GetRandomNodeForStart()
    {
        return DungeonNodeLib.GetNode("TestBattle");
    }

    /// <summary>
    /// 抽取BOSS节点
    /// </summary>
    /// <returns>BOSS节点</returns>
    public DungeonNode GetRandomNodeForBoss()
    {
        return DungeonNodeLib.GetNode("TestBattle");
    }

    /// <summary>
    /// 抽取遭遇战节点
    /// </summary>
    /// <param name="depth">节点深度</param>
    /// <returns>遭遇战节点</returns>
    public DungeonNode GetRandomNodeForEncounter(int depth)
    {
        return DungeonNodeLib.GetNode("TestBattle");
    }

    /// <summary>
    /// 抽取奖励事件节点
    /// </summary>
    /// <param name="depth">节点深度</param>
    /// <returns>奖励事件节点</returns>
    public DungeonNode GetRandomNodeForAwardEvent(int depth)
    {
        return DungeonNodeLib.GetNode("TestEvent");
    }

    /// <summary>
    /// 抽取挑战事件节点
    /// </summary>
    /// <param name="depth">节点深度</param>
    /// <returns>挑战事件节点</returns>
    public DungeonNode GetRandomNodeForChallengeEvent(int depth)
    {
        return DungeonNodeLib.GetNode("TestEvent");
    }

    /// <summary>
    /// 抽取撤离事件节点
    /// </summary>
    /// <param name="depth">节点深度</param>
    /// <returns>撤离事件节点</returns>
    public RestNode GetRandomNodeForRestEvent(int depth)
    {
        return DungeonNodeLib.GetRestNode("TestRest");
    }
}

public enum DungeonMapMode
{
    DUNGEON,
    BOSSFIGHT,
}
