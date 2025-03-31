using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestNode : DungeonNode
{
    /// <summary>
    /// 接下来的数条路、每条路上的数个节点
    /// </summary>
    public List<List<DungeonNode>> nextNodes = new List<List<DungeonNode>>();

    /// <summary>
    /// 设置接下来路上的节点
    /// </summary>
    public void SetNextNodes()
    {
        nextNodes[0].Add(connectedNodes[0]);
    }
}
