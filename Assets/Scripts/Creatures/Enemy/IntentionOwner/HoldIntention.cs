using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldIntention : MonoBehaviour
{
    /// <summary>
    /// 敌人组件
    /// </summary>
    EnemyBehaviour enemy;

    /// <summary>
    /// 将会被触发的意图
    /// </summary>
    IntentionBehaviour intention
    { 
        get{ return enemy.intention;}
        set{ enemy.intention = value;}
    }

    public Transform intentionOffset;

    /// <summary>
    /// 将当前的意图设置到给定的位置并显示
    /// </summary>
    public void SetIntentionPosition()
    {
        intention.transform.SetParent(intentionOffset.transform, false);
    }

    /// <summary>
    /// 清除目前显示的所有意图物体
    /// </summary>
    public void ClearIntention()
    {
        intention = null;
        Destroy(intention.gameObject);
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
        intention = Instantiate(IntentionLib.prefabs[ID]);
        intention.source = enemy;
        intention.target = null;
        intention.Amount = amount;

        switch (intention.TargetType)
        {
            case TargetType.SELF:
                intention.target = enemy;
                break;
            case TargetType.PLAYER:
                intention.target = DungeonManager.Instance.Player;
                break;
        }

        // if (enemy.buffOwner.HasBuff("Stun"))
        // {
        //     intention = Instantiate(IntentionLib.prefabs["StunIntention"]);
        // }
    }

    /// <summary>
    /// 触发意图
    /// </summary>
    public void TriggerIntention()
    {
        intention.ActOnEnemyTurn();
    }

    void Awake() 
    {
        enemy = GetComponent<EnemyBehaviour>();
    }
}
