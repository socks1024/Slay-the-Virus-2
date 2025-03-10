using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    [HideInInspector]
    public BoardBehaviour board;

    public Transform boardRoot;

    public CardFlowController cardFlow;

    public EnemyGroup enemyGroup;

    public PlayerBehaviour player;

    public int turnCount = 1;


    public void Start()
    {
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, OnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, OnAllActEnd);
        EventCenter.Instance.AddEventListener(EventType.PLAYER_DEAD, OnPlayerDead);
    }

    /// <summary>
    /// 初始化遭遇战并开始战斗
    /// </summary>
    public void InitializeEncounter(List<EnemyBehaviour> enemies)
    {
        board = player.board;
        board.transform.SetParent(boardRoot, false);
        cardFlow.FillDrawPile(player.deck);

        enemies.ForEach(e => {enemyGroup.AddEnemyToBattle(e,0);});
    }

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    public void OnBattleStart()
    {
        EventCenter.Instance.TriggerEvent(EventType.TURN_START);
    }

    /// <summary>
    /// 在所有行动结束时触发
    /// </summary>
    public void OnAllActEnd()
    {
        turnCount += 1;
        EventCenter.Instance.TriggerEvent(EventType.TURN_START);
    }

    /// <summary>
    /// 战斗胜利时触发的回调
    /// </summary>
    public void OnBattleWin()
    {
        board.transform.parent = null;
    }

    /// <summary>
    /// 战斗失败时触发的回调
    /// </summary>
    public void OnPlayerDead()
    {

    }
}
