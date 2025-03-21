using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoSingleton<DungeonManager>
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

    #endregion

    #region event management

    /// <summary>
    /// 事件处理脚本
    /// </summary>
    public EventManager eventManager;

    #endregion

    public void Start()
    {
        
    }

    /// <summary>
    /// 开启一场测试战斗
    /// </summary>
    /// <param name="deck">玩家牌组</param>
    /// <param name="board">玩家使用的棋盘</param>
    /// <param name="enemies">所有敌人</param>
    public void EnterBattleForTest(List<CardBehaviour> p_deck, BoardBehaviour p_board, List<EnemyBehaviour> p_enemies)
    {
        List<CardBehaviour> deck = InstantiateHelper.MultipleInstatiate<CardBehaviour>(p_deck);
        BoardBehaviour board = Instantiate(p_board);

        Player.SetBackpack(deck,board,null,0);
        battleManager.InitializeEncounter(p_enemies);
        battleManager.gameObject.SetActive(true);
        eventManager.gameObject.SetActive(false);
        EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
    }
}
