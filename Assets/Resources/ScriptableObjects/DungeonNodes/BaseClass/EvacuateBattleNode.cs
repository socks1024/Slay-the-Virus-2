using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvacuateBattleNode", menuName = "ScriptableObject/DungeonNode/EvacuateBattleNode")]
/// <summary>
/// 撤离时的无尽耐久战斗
/// </summary>
public class EvacuateBattleNode : BattleNode
{
    /// <summary>
    /// 第二批及之后的敌人预制体，最后一批敌人会重复出现
    /// </summary>
    [Header("补充敌人")]
    public List<EnemyBehaviour> p_AfterEnemies_1;
    public List<EnemyBehaviour> p_AfterEnemies_2;
    public List<EnemyBehaviour> p_AfterEnemies_3;
    public List<EnemyBehaviour> p_AfterEnemies_4;
    public List<EnemyBehaviour> p_AfterEnemies_5;
    public List<EnemyBehaviour> p_AfterEnemies_6;
    public List<EnemyBehaviour> p_AfterEnemies_7;
    public List<EnemyBehaviour> p_AfterEnemies_8;

    /// <summary>
    /// 需要坚持的回合数
    /// </summary>
    public int enduranceTurn;

    public List<List<EnemyBehaviour>> p_AfterEnemies
    {
        get
        {
            List<List<EnemyBehaviour>> result = new List<List<EnemyBehaviour>>();

            if (p_AfterEnemies_1 != null) result.Add(p_AfterEnemies_1);
            if (p_AfterEnemies_2 != null) result.Add(p_AfterEnemies_2);
            if (p_AfterEnemies_3 != null) result.Add(p_AfterEnemies_3);
            if (p_AfterEnemies_4 != null) result.Add(p_AfterEnemies_4);
            if (p_AfterEnemies_5 != null) result.Add(p_AfterEnemies_5);
            if (p_AfterEnemies_6 != null) result.Add(p_AfterEnemies_6);
            if (p_AfterEnemies_7 != null) result.Add(p_AfterEnemies_7);
            if (p_AfterEnemies_8 != null) result.Add(p_AfterEnemies_8);
            
            return result;
        }
    }
}
