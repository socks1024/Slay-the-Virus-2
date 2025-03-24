using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoSingletonDestroyOnLoad<DungeonManager>
{
    #region dungeon process

    //进入副本的玩家的数据：携带的卡牌等
    //副本的数据：地图的生成逻辑是什么，包含哪些事件，可能产生哪些遭遇战，最终BOSS是什么

    //玩家：赋值玩家的deck，board，items，初始化生命值和变量

    //地图生成算法（单独一个类？)

    //地图事件（数据Info类 + 工厂模式？由地图生成算法产出，包括事件数据类和遭遇战数据类等等）

    /// <summary>
    /// 玩家数据
    /// </summary>
    public PlayerBehaviour Player{ get { return battleManager.player; } }

    #endregion

    #region battle management

    /// <summary>
    /// 战斗处理器
    /// </summary>
    public BattleManager battleManager;

    /// <summary>
    /// 触发战斗
    /// </summary>
    /// <param name="battleInfo">战斗所需的信息</param>
    public void EnterBattle(DungeonBattleInfo battleInfo)
    {
        battleManager.InitializeEncounter(battleInfo);

        battleManager.gameObject.SetActive(true);
        eventManager.gameObject.SetActive(false);

        EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
    }

    /// <summary>
    /// 清除所有回合内触发的回调
    /// </summary>
    public void ClearAllInTurnEvent()
    {
        EventCenter.Instance.RemoveAllEventListener(EventType.TURN_START);
        EventCenter.Instance.RemoveAllEventListener(EventType.ACT_START);
        EventCenter.Instance.RemoveAllEventListener(EventType.CARD_ACT_END);
        EventCenter.Instance.RemoveAllEventListener(EventType.ENEMY_ACT_END);
    }

    /// <summary>
    /// 清除所有与战斗相关的回调
    /// </summary>
    public void ClearAllBattleEvent()
    {
        ClearAllInTurnEvent();
        EventCenter.Instance.RemoveAllEventListener(EventType.BATTLE_START);
        EventCenter.Instance.RemoveAllEventListener(EventType.PLAYER_DEAD);
        EventCenter.Instance.RemoveAllEventListener(EventType.SINGLE_ENEMY_KILLED);
        EventCenter.Instance.RemoveAllEventListener(EventType.BATTLE_WIN);
    }

    #endregion

    #region event management

    /// <summary>
    /// 事件处理脚本
    /// </summary>
    public EventManager eventManager;

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventInfo">事件所需的信息</param>
    public void EnterEvent(DungeonEventInfo eventInfo)
    {
        eventManager.SetEvent(eventInfo);

        battleManager.gameObject.SetActive(false);
        eventManager.gameObject.SetActive(true);
    }

    #endregion

    protected override void Init()
    {
        ClearAllBattleEvent();
    }

    #region test

    /// <summary>
    /// 开启一场测试战斗
    /// </summary>
    /// <param name="deck">玩家牌组</param>
    /// <param name="board">玩家使用的棋盘</param>
    /// <param name="enemies">所有敌人</param>
    public void EnterBattleForTest(List<CardBehaviour> p_deck, BoardBehaviour p_board, List<EnemyBehaviour> p_enemies)
    {
        Player.SetBackpack(p_deck,p_board,null,0);

        battleManager.InitializeEncounter(p_enemies);

        battleManager.gameObject.SetActive(true);
        eventManager.gameObject.SetActive(false);

        EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
    }

    /// <summary>
    /// 开启一场测试战斗
    /// </summary>
    /// <param name="infoTest">战斗数据</param>
    public void EnterBattleForTest(EnterBattleInfoTest infoTest)
    {
        EnterBattleForTest(infoTest.p_Cards, infoTest.p_Board, infoTest.p_Enemies);
    }

    #endregion

    
}
