using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HoldIntention))]
[RequireComponent(typeof(AnimateIntention))]
public abstract class SimpleEnemyBehaviour : CreatureBehaviour<SimpleEnemyData>
{
    /// <summary>
    /// 可以触发的所有意图
    /// </summary>
    public List<IntentionBehaviour> IntentionsAvailable;

    public void ActOnBattleStart()
    {

    }

    public void ActOnEnemyTurn()
    {
        GetComponent<HoldIntention>();
    }

    void Start()
    {
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, ActOnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.CARD_ACT_END, ActOnEnemyTurn);
    }

}
