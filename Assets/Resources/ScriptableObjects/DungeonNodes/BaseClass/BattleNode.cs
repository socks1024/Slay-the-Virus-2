using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleNode", menuName = "ScriptableObject/DungeonNode/BattleNode")]
/// <summary>
/// 战斗节点
/// </summary>
public class BattleNode : DungeonNode
{
    /// <summary>
    /// 敌人预制体列表
    /// </summary>
    public List<EnemyBehaviour> p_Enemies;
}
