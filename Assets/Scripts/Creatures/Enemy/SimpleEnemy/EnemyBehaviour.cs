using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HoldIntention))]
[RequireComponent(typeof(AnimateIntention))]
[RequireComponent(typeof(SimpleEnemyShow))]
public abstract class EnemyBehaviour : CreatureBehaviour<SimpleEnemyData>
{
    /// <summary>
    /// 可以触发的所有意图
    /// </summary>
    public List<IntentionBehaviour> IntentionsAvailable;

    [HideInInspector]
    /// <summary>
    /// 将会被触发的意图
    /// </summary>
    public IntentionBehaviour intention;

    /// <summary>
    /// 敌人图片
    /// </summary>
    public Sprite sprite{ get{ return creatureData.EnemySprite; } }

    /// <summary>
    /// 该敌人经历的总回合数
    /// </summary>
    int turnCount = 0;

    /// <summary>
    /// 在战斗开始时被调用
    /// </summary>
    public virtual void ActOnBattleStart()
    {
        
    }

    /// <summary>
    /// 在每个回合开始时被调用
    /// </summary>
    public virtual void ActOnTurnStart()
    {
        //设置意图
        SetIntention(turnCount);
        GetComponent<AnimateIntention>().SetIntentionPosition();
    }

    /// <summary>
    /// 在每回合轮到敌人行动时被调用
    /// </summary>
    public virtual void ActOnEnemyMove()
    {
        //执行意图
        GetComponent<HoldIntention>().TriggerIntention();
        GetComponent<AnimateIntention>().PlayIntentionAnimation();

        //消去意图
        GetComponent<HoldIntention>().ClearIntention();
        GetComponent<AnimateIntention>().SetIntentionPosition();
    }

    protected override void Start()
    {
        base.Start();
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, ActOnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ActOnTurnStart);
        EventCenter.Instance.AddEventListener(EventType.CARD_ACT_END, ActOnEnemyMove);
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, () => turnCount++ );
    }

    /// <summary>
    /// 根据当前回合数设置意图
    /// </summary>
    /// <param name="turnCount">回合数</param>
    public abstract void SetIntention(int turnCount);

}
