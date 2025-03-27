using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvacuateEventNodeInfo", menuName = "ScriptableObject/DungeonNode/EvacuateEventNodeInfo")]
/// <summary>
/// 撤离节点
/// </summary>
public class EvacuateEventNodeInfo : EventNodeInfo
{
    /// <summary>
    /// 撤离按钮的描述
    /// </summary>
    public string evacuateButtonText;

    /// <summary>
    /// 该撤离点导向的撤离战斗
    /// </summary>
    public EvacuateBattleNodeInfo evacuateBattle;
}
