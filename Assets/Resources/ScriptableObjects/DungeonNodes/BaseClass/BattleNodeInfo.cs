using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleNodeInfo", menuName = "ScriptableObject/DungeonNodeInfo/BattleNodeInfo")]
/// <summary>
/// 战斗节点
/// </summary>
public class BattleNodeInfo : DungeonNodeInfo
{
    /// <summary>
    /// 敌人预制体列表
    /// </summary>
    public List<EnemyBehaviour> p_Enemies;

    /// <summary>
    /// 战利品库
    /// </summary>
    public LootInfo lootInfo;
}
