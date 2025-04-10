using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HoldIntention))]
[RequireComponent(typeof(AnimateIntention))]
public abstract class EnemyBehaviour : CreatureBehaviour
{
    # region intents

    /// <summary>
    /// 敌人图片
    /// </summary>
    public Sprite enemySprite;

    /// <summary>
    /// 可以触发的所有意图
    /// </summary>
    public List<IntentionBehaviour> IntentionPrefabsAvailable;

    /// <summary>
    /// 将会被触发的意图
    /// </summary>
    [HideInInspector]public IntentionBehaviour intention;

    /// <summary>
    /// 该敌人经历的总回合数，从 1 开始计算，每回合结束 + 1
    /// </summary>
    protected int turnCount{ get{ return DungeonManager.Instance.battleManager.turnCount; }}

    /// <summary>
    /// 意图逻辑处理组件
    /// </summary>
    protected HoldIntention holdIntention;

    /// <summary>
    /// 意图显示处理组件
    /// </summary>
    protected AnimateIntention animateIntention;

    #endregion

    /// <summary>
    /// 在每个回合开始时被调用
    /// </summary>
    public virtual void ActOnTurnStart()
    {
        //设置意图
        SetIntention(turnCount);
        animateIntention.SetIntentionPosition();
    }

    /// <summary>
    /// 在每回合轮到敌人行动时被调用
    /// </summary>
    public virtual void ActOnEnemyMove()
    {
        //执行意图
        holdIntention.TriggerIntention();
        animateIntention.PlayIntentionAnimation();
    }

    /// <summary>
    /// 在敌人行动结束之后调用
    /// </summary>
    public virtual void ActOnEnemyTurnEnd()
    {
        //消去意图
        animateIntention.ClearIntention();
        holdIntention.ClearIntention();
    }

    protected override void Awake()
    {
        base.Awake();
        holdIntention = GetComponent<HoldIntention>();
        animateIntention = GetComponent<AnimateIntention>();
        takeDamage.ActOnDead += () => DungeonManager.Instance.battleManager.enemyGroup.DestroyEnemyFromBattle(this);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ActOnTurnStart);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, ActOnTurnStart);
    }

    /// <summary>
    /// 根据当前回合数设置意图
    /// </summary>
    /// <param name="turnCount">回合数</param>
    public virtual void SetIntention(int turnCount)
    {
        if (buffOwner.HasBuff("Stun"))
        {
            // 被眩晕
        }
    }

    /// <summary>
    /// 进入战斗时调用
    /// </summary>
    public abstract void ActOnEnterBattle();

}
