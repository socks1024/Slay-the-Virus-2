using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RelicUI))]
public abstract class RelicBehaviour : MonoBehaviour
{
    // /// <summary>
    // /// 道具的数据
    // /// </summary>
    // public ItemData item;

    /// <summary>
    /// 道具的ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 检视道具时显示的名称
    /// </summary>
    public string Name;

    [TextArea]
    /// <summary>
    /// 检视道具时显示的描述
    /// </summary>
    public string Description;

    /// <summary>
    /// 道具的外形
    /// </summary>
    public Sprite sprite;

    protected PlayerBehaviour Player{ get{ return DungeonManager.Instance.Player; }}

    protected bool BoardAllFilled
    {
        get 
        { 
            return DungeonManager.Instance.battleManager.board.IsAllFilled(); 
        }
    }

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    public virtual void ActOnBattleStart()
    {
        print("Relic act on battle start");
    }

    /// <summary>
    /// 回合开始时触发的回调
    /// </summary>
    public virtual void ActOnTurnStart()
    {
        print("Relic act on turn start");
    }

    /// <summary>
    /// 出牌时触发的回调
    /// </summary>
    public virtual void ActOnCardAct()
    {
        print("Relic act on card act");
    }

    /// <summary>
    /// 敌人死亡时触发的回调
    /// </summary>
    public virtual void ActOnEnemyDead()
    {
        print("Relic act on enemy dead");
    }

    protected virtual void Awake()
    {
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, ActOnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ActOnTurnStart);
        // EventCenter.Instance.AddEventListener(EventType.ACT_START, ActOnCardAct);
        EventCenter.Instance.AddEventListener(EventType.SINGLE_ENEMY_KILLED, ActOnEnemyDead);

        GetComponent<RelicUI>().image.sprite = sprite;
    }

    protected virtual void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_START, ActOnBattleStart);
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, ActOnTurnStart);
        // EventCenter.Instance.RemoveEventListener(EventType.ACT_START, ActOnCardAct);
        EventCenter.Instance.RemoveEventListener(EventType.SINGLE_ENEMY_KILLED, ActOnEnemyDead);
    }
}
