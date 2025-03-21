using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [HideInInspector] public BattleInfo battleInfo;

    [HideInInspector] public BoardBehaviour board;

    public Transform boardRoot;

    public CardFlowController cardFlow;

    public EnemyGroup enemyGroup;

    public PlayerBehaviour player;

    [HideInInspector] public int turnCount = 1;

    #region battle start phase

    /// <summary>
    /// 初始化遭遇战并开始战斗
    /// </summary>
    public void InitializeEncounter(BattleInfo battleInfo)
    {
        //还没有添加道具初始化
        //还没有添加战利品初始化

        board = player.board;
        board.transform.SetParent(boardRoot, false);
        cardFlow.FillDrawPile(player.deck);

        List<EnemyBehaviour> enemyBehaviours = new List<EnemyBehaviour>();
        battleInfo.enemies.ForEach(e => enemyBehaviours.Add(e.GetComponent<EnemyBehaviour>()));
        InstantiateHelper.MultipleInstatiate<EnemyBehaviour>(enemyBehaviours);
        enemyBehaviours.ForEach(e => {enemyGroup.AddEnemyToBattle(e,0);});
    }

    /// <summary>
    /// 初始化遭遇战
    /// </summary>
    /// <param name="p_enemies">敌人预制体列表</param>
    public void InitializeEncounter(List<EnemyBehaviour> p_enemies)
    {
        //还没有添加道具初始化
        //还没有添加战利品初始化

        board = player.board;
        board.transform.SetParent(boardRoot, false);
        cardFlow.FillDrawPile(player.deck);

        InstantiateHelper.MultipleInstatiate<EnemyBehaviour>(p_enemies).ForEach(e => {enemyGroup.AddEnemyToBattle(e,0);});
    }

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    void OnBattleStart()
    {
        EventCenter.Instance.TriggerEvent(EventType.TURN_START);
    }

    #endregion

    #region turn logic phase

    // BATTLE_START:
    // TURN_START:
    // ACT_START:按下按钮触发
    // CARD_ACT_END:弃牌动画播放完毕 && 所有卡牌动画播放完毕
    // ENEMY_ACT_END:所有敌人动画播放完毕

    /// <summary>
    /// 弃牌动画是否已经结束
    /// </summary>
    public bool DiscardAnimFinished
    {
        get{ return discardAnimFinished; }
        set
        {
            discardAnimFinished = value;
            if (discardAnimFinished && playAnimFinished)
            {
                TriggerCardActEnd();
            }
        }
    }
    bool discardAnimFinished;

    /// <summary>
    /// 出牌动画是否已经结束
    /// </summary>
    public bool PlayAnimFinished
    {
        get{ return playAnimFinished; }
        set
        {
            playAnimFinished = value;
            if (discardAnimFinished && playAnimFinished)
            {
                TriggerCardActEnd();
            }
        }
    }
    bool playAnimFinished;

    /// <summary>
    /// 当 弃牌动画播放完毕 && 所有卡牌动画播放完毕 时，触发卡牌行动结束
    /// </summary>
    void TriggerCardActEnd()
    {
        EventCenter.Instance.TriggerEvent(EventType.CARD_ACT_END);
        discardAnimFinished = false;
        playAnimFinished = false;
    }

    /// <summary>
    /// 在所有行动结束时触发
    /// </summary>
    void OnAllActEnd()
    {
        turnCount += 1;
        EventCenter.Instance.TriggerEvent(EventType.TURN_START);
        battleInfo?.OnAllActEndCallback(turnCount);
    }

    #endregion

    /// <summary>
    /// 战斗胜利时触发的回调
    /// </summary>
    void OnBattleWin()
    {
        board.transform.parent = null;
        //获得战利品
    }

    /// <summary>
    /// 战斗失败时触发的回调
    /// </summary>
    void OnPlayerDead()
    {
        //死回家直接结算
    }

    void Awake()
    {
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, OnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, OnAllActEnd);
        EventCenter.Instance.AddEventListener(EventType.BATTLE_WIN, OnBattleWin);
        EventCenter.Instance.AddEventListener(EventType.PLAYER_DEAD, OnPlayerDead);
    }
}
