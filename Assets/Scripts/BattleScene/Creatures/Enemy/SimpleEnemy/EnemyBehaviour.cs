using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(HoldIntention))]
// [RequireComponent(typeof(AnimateIntention))]
[RequireComponent(typeof(EnemyShow))]
public abstract class EnemyBehaviour : CreatureBehaviour
{
    #region intents

    protected PlayerBehaviour Player{ get{ return DungeonManager.Instance.Player; } }

    /// <summary>
    /// 敌人图片
    /// </summary>
    public Sprite enemySprite;

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

    #endregion

    public EnemyType type;

    /// <summary>
    /// 在每个回合开始时被调用
    /// </summary>
    public virtual void ActOnTurnStart()
    {
        //设置意图
        EnemyChooseIntention(turnCount);
        holdIntention.SetIntentionPosition();
    }

    public virtual void ActBeforeCardAct()
    {
        
    }

    /// <summary>
    /// 在每回合轮到敌人行动时被调用
    /// </summary>
    public virtual void ActOnEnemyMove()
    {
        if (!buffOwner.HasBuff("Stun"))
        {
            //执行意图
            holdIntention.TriggerIntention();
            // holdIntention.PlayIntentionAnimation();
        }
    }

    /// <summary>
    /// 在敌人行动结束之后调用
    /// </summary>
    public virtual void ActOnEnemyTurnEnd()
    {
        //消去意图
        holdIntention.ClearIntention();
    }

    public override void GetDamage(int damage, bool blockable = false)
    {
        base.GetDamage(damage);
        AnimationManager.Instance.StartFlash(GetComponent<Image>());
    }

    protected override void Awake()
    {
        base.Awake();

        holdIntention = GetComponent<HoldIntention>();
        holdIntention.ClearIntention();
        takeDamage.ActOnDead += () => EventCenter.Instance.TriggerEvent(EventType.SINGLE_ENEMY_KILLED);
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
    public abstract void EnemyChooseIntention(int turnCount);

    /// <summary>
    /// 设置具体意图
    /// </summary>
    /// <param name="info">意图信息</param>
    public void SetIntention(IntentionInfo info)
    {
        holdIntention.SetIntention(info);
    }

    /// <summary>
    /// 进入战斗时调用
    /// </summary>
    public abstract void ActOnEnterBattle();

    /// <summary>
    /// 死亡时调用
    /// </summary>
    public override void OnDead()
    {
        DungeonManager.Instance.battleManager.enemyGroup.DestroyEnemyFromBattle(this);
    }

}

public enum EnemyType
{
    BOSS,
    ELITE,
    NORMAL,
}
