using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRoads : MonoBehaviour
{
    /// <summary>
    /// 这条道路通向的所有节点
    /// </summary>
    List<DungeonNode> nodesOfRoad;

    /// <summary>
    /// 事件节点的图片
    /// </summary>
    [SerializeField] Sprite EventNodeSprite;

    /// <summary>
    /// 战斗节点的图片
    /// </summary>
    [SerializeField] Sprite BattleNodeSprite;

    /// <summary>
    /// 节点显示用UI物体
    /// </summary>
    [SerializeField] GameObject DungeonNodePrefab;

    /// <summary>
    /// 已经被显示的地图节点
    /// </summary>
    List<GameObject> actualNodes = new List<GameObject>();

    /// <summary>
    /// 设置道路
    /// </summary>
    /// <param name="nodesOfRoad">道路中的所有节点</param>
    public void SetUpRoad(List<DungeonNode> nodesOfRoad)
    {
        this.nodesOfRoad = nodesOfRoad;

        GetComponent<Image>().raycastTarget = true;
        
        nodesOfRoad.ForEach(node => ShowDungeonNode(node.nodeType));
    }

    /// <summary>
    /// 为道路添加节点显示
    /// </summary>
    /// <param name="type"></param>
    void ShowDungeonNode(DungeonNodeType type)
    {
        Sprite s = null;

        switch (type)
        {
            case DungeonNodeType.BATTLE:
                s = BattleNodeSprite;
                break;
            case DungeonNodeType.EVENT:
                s = EventNodeSprite;
                break;
        }

        GameObject nodeUI = Instantiate(DungeonNodePrefab);
        nodeUI.transform.SetParent(transform);
        nodeUI.GetComponent<Image>().sprite = s;
        nodeUI.GetComponent<Image>().raycastTarget = false;

        actualNodes.Add(nodeUI);
    }

    /// <summary>
    /// 当被点击选择时触发的回调
    /// </summary>
    public void OnChooseRoad()
    {
        DungeonManager.Instance.EnterNode(nodesOfRoad[0]);
    }

    /// <summary>
    /// 清空并重新初始化
    /// </summary>
    public void ClearNodes()
    {
        nodesOfRoad = null;

        for (int i = actualNodes.Count - 1; i >= 0; i--)
        {
            Destroy(actualNodes[i]);
        }
    }
}
