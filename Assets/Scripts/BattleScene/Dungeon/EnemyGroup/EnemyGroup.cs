using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyGroupUI))]
public class EnemyGroup : MonoBehaviour
{
    /// <summary>
    /// 本场战斗的全部敌人，按照行动顺序排列
    /// </summary>
    [HideInInspector]public List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();

    UnityAction<List<EnemyBehaviour>> OnEnemyAmountChange;

    public EnemyBehaviour HoveredEnemy
    {
        get { return hoveredEnemy;}
        set 
        {
            if (hoveredEnemy != null)
            {
                hoveredEnemy.GetComponent<EnemyShow>().ExitHighlight();
            }

            hoveredEnemy = value;

            if (hoveredEnemy != null)
            {
                hoveredEnemy.GetComponent<EnemyShow>().EnterHighlight();
            }
        }
    }
    EnemyBehaviour hoveredEnemy;

    void Awake()
    {
        OnEnemyAmountChange = GetComponent<EnemyGroupUI>().OnEnemyAmountChange;

        EventCenter.Instance.AddEventListener(EventType.CARD_ACT_END, EnemyAct);
        EventCenter.Instance.AddEventListener(EventType.BATTLE_WIN, ClearEnemy);
    }

    void Update()
    {
        HoveredEnemy = GetHoveredEnemy();
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.CARD_ACT_END, EnemyAct);
        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_WIN, ClearEnemy);
    }

    /// <summary>
    /// 获取遭遇战中的敌人
    /// </summary>
    /// <param name="Id">敌人ID</param>
    /// <returns>获取到的敌人</returns>
    public EnemyBehaviour GetEnemyByID(string Id)
    {
        foreach (EnemyBehaviour enemy in enemies)
        {
            if (enemy.ID == Id)
            {
                return enemy;
            }
        }

        return null;
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
    /// 获取随机敌人
    /// </summary>
    /// <returns>随机到的敌人</returns>
    public EnemyBehaviour GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    /// <summary>
    /// 获取敌人数量
    /// </summary>
    /// <returns>敌人数量</returns>
    public int GetEnemyCount()
    {
        return enemies.Count;
    }

    /// <summary>
    /// 将敌人添加到战斗中
    /// </summary>
    /// <param name="enemy">要添加的敌人</param>
    /// <param name="moveIndex">敌人的行动顺序（从0开始）</param>
    public void AddEnemyToBattle(EnemyBehaviour enemy, int moveIndex)
    {
        enemy.transform.SetParent(transform, false);
        enemies.Insert(moveIndex, enemy);
        enemy.ActOnEnterBattle();

        OnEnemyAmountChange.Invoke(enemies);
    }

    /// <summary>
    /// 将敌人添加到战斗中
    /// </summary>
    /// <param name="enemy">要添加的敌人</param>
    public void AddEnemyToBattle(EnemyBehaviour enemy)
    {
        enemy.transform.SetParent(transform, false);
        enemies.Add(enemy);
        enemy.ActOnEnterBattle();

        OnEnemyAmountChange.Invoke(enemies);
    }

    bool allEnemyDestroyed = false;

    /// <summary>
    /// 将敌人从战斗中销毁
    /// </summary>
    /// <param name="enemy">要销毁的敌人</param>
    public void DestroyEnemyFromBattle(EnemyBehaviour enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Destroy(enemy.gameObject);

            OnEnemyAmountChange.Invoke(enemies);

            if (!allEnemyDestroyed && (enemy.type == EnemyType.BOSS || enemies.Count == 0))
            {
                allEnemyDestroyed = true;
                DungeonManager.Instance.battleManager.OnAllEnemyDestroyed();
            }
        }
    }

    /// <summary>
    /// 敌人行动
    /// </summary>
    public void EnemyAct()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (!enemies[i].buffOwner.HasBuff("Stun"))
            {
                enemies[i].ActOnEnemyMove();
            }
        }
        //在这之间加动画？
        TriggerEnemyActEnd();
    }

    /// <summary>
    /// 敌人优先的行动
    /// </summary>
    public void EnemyActBeforCardAct()
    {
        foreach(EnemyBehaviour enemy in enemies)
        {
            if (!enemy.buffOwner.HasBuff("Stun"))
            {
                enemy.ActBeforeCardAct();
            }
        }
        //在这之间加动画？
    }

    /// <summary>
    /// 敌人行动结束时调用
    /// </summary>
    public void TriggerEnemyActEnd()
    {
        enemies.ForEach(enemy => { enemy.ActOnEnemyTurnEnd(); });
        
        EventCenter.Instance.TriggerEvent(EventType.ENEMY_ACT_END);
    }

    /// <summary>
    /// 清除所有敌人
    /// </summary>
    public void ClearEnemy()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            DestroyEnemyFromBattle(enemies[i]);
        }
    }
}
