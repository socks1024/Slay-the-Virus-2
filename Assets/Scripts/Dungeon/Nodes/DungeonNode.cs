using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNode
{
    /// <summary>
    /// 节点数据的 Scriptable Object
    /// </summary>
    public DungeonNodeInfo nodeInfo;

    /// <summary>
    /// 该节点连接的其它节点
    /// </summary>
    public List<DungeonNode> connectedNodes = new List<DungeonNode>();

    /// <summary>
    /// 该节点是否被访问过
    /// </summary>
    public bool visited = false;
}
