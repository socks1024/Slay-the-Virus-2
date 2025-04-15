using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] protected IntentionBehaviour intentionPrefab;

    /// <summary>
    /// 将当前的意图设置到给定的位置并显示
    /// </summary>
    public void SetIntentionPosition()
    {
        intention.transform.SetParent(intentionOffset.transform, false);
    }

    /// <summary>
    /// 清除目前显示的意图物体
    /// </summary>
    public void ClearIntention()
    {
        Destroy(intention.gameObject);
        // intention = null;
    }

    /// <summary>
    /// 设置意图
    /// </summary>
    /// <param name="type">意图类型</param>
    /// <param name="showText">意图文字</param>
    /// <param name="action">意图行为</param>
    public void SetIntention(IntentionType type, string showText, UnityAction action)
    {
        intention = Instantiate(intentionPrefab);
        intention.SetIntention(type, showText, action);
    }

    /// <summary>
    /// 触发意图
    /// </summary>
    public void TriggerIntention()
    {
        intention.ActOnEnemyTurn.Invoke();
    }

    void Awake() 
    {
        enemy = GetComponent<EnemyBehaviour>();
    }
}
