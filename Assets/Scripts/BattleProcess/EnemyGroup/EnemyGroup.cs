using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGroup : MonoBehaviour
{
    /// <summary>
    /// 本场战斗的全部敌人，按照行动顺序排列
    /// </summary>
    List<EnemyBehaviour> enemies;

    void Start()
    {
        foreach (EnemyBehaviour enemy in GetComponentsInChildren<EnemyBehaviour>())
        {
            enemies.Add(enemy);
        }
        EventCenter.Instance.AddEventListener(EventType.CARD_ACT_END, EnemyAct);
    }

    /// <summary>
    /// 获取鼠标下的敌人
    /// </summary>
    /// <returns>鼠标下的敌人，若没有的话返回null</returns>
    public EnemyBehaviour GetHoveredEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("SimpleEnemy"))
            {
                return hit.collider.GetComponent<EnemyBehaviour>();
            }
        }

        return null;
    }

    /// <summary>
    /// 将敌人添加到战斗中
    /// </summary>
    /// <param name="enemy">要添加的敌人</param>
    /// <param name="moveIndex">敌人的行动顺序（从0开始）</param>
    public void AddEnemyToBattle(EnemyBehaviour enemy, int moveIndex)
    {
        enemies.Insert(moveIndex, enemy);
    }

    /// <summary>
    /// 将敌人从战斗中移除
    /// </summary>
    /// <param name="enemy">要移除的敌人</param>
    public void RemoveEnemyFromBattle(EnemyBehaviour enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            EventCenter.Instance.TriggerEvent(EventType.BATTLE_WIN);
        }
    }

    /// <summary>
    /// 敌人行动
    /// </summary>
    public void EnemyAct()
    {
        foreach(EnemyBehaviour enemy in enemies)
        {
            enemy.ActOnEnemyMove();
        }
    }
}
