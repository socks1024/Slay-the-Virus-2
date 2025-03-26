using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("地图参数")]
    public int layers = 5;
    public int roadsPerLayer = 3;
    public int minNodesPerRoad = 2;
    public int maxNodesPerRoad = 4;
    // public float layerSpacing = 3f;
    // public float nodeSpacing = 2f;

    [Header("节点类型权重")]
    public int battleNodeChance;
    public int awardNodeChance;
    public int challengeNodeChance;

    private List<List<List<DungeonNode>>> mapLayers = new List<List<List<DungeonNode>>>();
    private List<DungeonNode> evacuateNodes = new List<DungeonNode>();
    private DungeonNode startNode;
    private DungeonNode bossNode;

    /// <summary>
    /// 将mapLayers填充为正式地图
    /// </summary>
    public void GenerateMap()
    {
        mapLayers.Clear();

        startNode = null;//添加起始遭遇战节点

        for (int i = 0; i < layers; i++)
        {
            evacuateNodes.Add(null);//添加撤离节点

            //将上一个 Layer 的最后一列节点连接到新撤离节点
            if (i == 0)
            {
                startNode.connectedNodes.Add(evacuateNodes[i]);
            }
            else
            {
                mapLayers[i - 1].ForEach(road => road[road.Count-1].connectedNodes.Add(evacuateNodes[i]));
            }

            List<List<DungeonNode>> currentLayer = new List<List<DungeonNode>>();
            int nodesInRoad = Random.Range(minNodesPerRoad, maxNodesPerRoad + 1);
            
            //一共 roadsPerLayer 条岔路
            for (int j = 0; j < roadsPerLayer; j++)
            {
                //每条岔路上有 minNodesPerRoad 到 maxNodesPerRoad 个节点
                for (int k = 0; k < nodesInRoad; k++)
                {
                    DungeonNode newNode;
                
                    newNode = GetRandomNode(i);
                    
                    currentLayer[j].Add(newNode);

                    //将新节点与前一个节点连接
                    if (k == 0)
                    {
                        evacuateNodes[i].connectedNodes.Add(newNode);
                    }
                    else
                    {
                        currentLayer[j][k-1].connectedNodes.Add(newNode);
                    }
                }
            }
            
            mapLayers.Add(currentLayer);
        }

        bossNode = null;//添加BOSS战节点

        //将BOSS战节点连接到上一列节点
        mapLayers[mapLayers.Count - 1].ForEach(road => road[road.Count-1].connectedNodes.Add(bossNode));
    }

    /// <summary>
    /// 抽取中间节点
    /// </summary>
    /// <returns>下一个中间节点</returns>
    public DungeonNode GetRandomNode(int mapDepth)
    {
        float r = Random.value;

        int totalWeight = battleNodeChance + awardNodeChance + challengeNodeChance;

        float battleChance = battleNodeChance / totalWeight;
        float awardChance = awardNodeChance / totalWeight;

        
        if (r < battleChance) 
            return null;//添加遭遇战节点
        if (r < battleChance + awardChance)
            return null;//添加奖励事件节点
        else
            return null;//添加挑战事件节点
    }
}
