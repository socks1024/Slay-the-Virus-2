using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestNodeInfo", menuName = "ScriptableObject/DungeonNodeInfo/RestNodeInfo")]
/// <summary>
/// 撤离节点
/// </summary>
public class RestNodeInfo : DungeonNodeInfo
{
    // /// <summary>
    // /// 撤离按钮的描述
    // /// </summary>
    // public string evacuateButtonText;

    /// <summary>
    /// 该撤离点导向的撤离战斗
    /// </summary>
    public EvacuateBattleNodeInfo evacuateBattle;
}
