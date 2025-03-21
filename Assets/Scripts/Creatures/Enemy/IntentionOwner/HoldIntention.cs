using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldIntention : MonoBehaviour
{
    /// <summary>
    /// 将会被触发的意图
    /// </summary>
    IntentionBehaviour intention
    { 
        get{ return GetComponent<EnemyBehaviour>().intention;}
        set{ GetComponent<EnemyBehaviour>().intention = value; }
    }

    /// <summary>
    /// 若一个特定的意图可用，将其设为将要触发的意图
    /// </summary>
    /// <param name="ID">该意图的ID</param>
    /// <param name="target">该意图的目标</param>
    /// <param name="amount">该意图的强度</param>
    public void SetIntention(string ID, CreatureBehaviour target, int amount = 0)
    {
        SetIntention(ID, amount);
        intention.target = target;
    }

    /// <summary>
    /// 若一个特定的意图可用，将其设为将要触发的意图
    /// </summary>
    /// <param name="ID">该意图的ID</param>
    /// <param name="amount">该意图的强度</param>
    public void SetIntention(string ID, int amount = 0)
    {
        foreach (IntentionBehaviour intentionPrefab in GetComponent<EnemyBehaviour>().IntentionPrefabsAvailable)
        {
            if(ID.Equals(intentionPrefab.ID))
            {
                intention = Instantiate(intentionPrefab);
                intention.source = GetComponent<EnemyBehaviour>();
                intention.target = null;
                intention.Amount = amount;
            }
        }

        switch (intention.targetType)
        {
            case TargetType.SELF:
                intention.target = GetComponent<EnemyBehaviour>();
                break;
            case TargetType.PLAYER:
                intention.target = DungeonManager.Instance.Player;
                break;
        }
    }

    /// <summary>
    /// 触发意图
    /// </summary>
    public void TriggerIntention()
    {
        intention.ActOnEnemyTurn();
    }

    /// <summary>
    /// 清除意图
    /// </summary>
    public void ClearIntention()
    {
        intention = null;
    }
}
