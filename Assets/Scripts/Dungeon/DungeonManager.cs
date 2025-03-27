using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoSingletonDestroyOnLoad<DungeonManager>
{
    #region dungeon data

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
    void EnterBattle(BattleNode battleInfo)
    {
        eventManager.gameObject.SetActive(false);

        RightBG.DOMove(rightBGBattlePos, BGMoveTime).OnComplete(() => {
            battleManager.gameObject.SetActive(true);
            battleManager.InitializeEncounter(battleInfo);
            EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
        });
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
    void EnterEvent(EventNode eventInfo)
    {
        battleManager.gameObject.SetActive(false);

        RightBG.DOMove(Vector3.zero, BGMoveTime).OnComplete(() => {
            eventManager.SetEvent(eventInfo);
            eventManager.gameObject.SetActive(true);
        });
    }

    #endregion

    #region dungeon UI

    [Header("UI")]
    public Transform LeftBG;
    public Transform RightBG;
    
    /// <summary>
    /// 右侧背景的战斗时位置
    /// </summary>
    Vector3 rightBGBattlePos = new Vector3(8,0,0);

    /// <summary>
    /// 背景移动花费的时间
    /// </summary>
    public float BGMoveTime = 0.5f;

    #endregion

    #region map

    MapGenerator mapGenerator;

    /// <summary>
    /// 进入下一个节点
    /// </summary>
    public void EnterNode(DungeonNode node)
    {
        if (node is BattleNode)
        {
            EnterBattle(node as BattleNode);
        }
        else if (node is EventNode)
        {
            EnterEvent(node as EventNode);
        }

        node.visited = true;
    }

    #endregion

    protected override void Init()
    {
        mapGenerator = GetComponent<MapGenerator>();
        ClearAllBattleEvent();
    }

    public void StartAdventure()
    {
        mapGenerator.GenerateMap();
        EnterNode(mapGenerator.startNode);
    }

    #region test

    /// <summary>
    /// 开启一场测试战斗
    /// </summary>
    /// <param name="deck">玩家牌组</param>
    /// <param name="board">玩家使用的棋盘</param>
    public void EnterBattleForTest(List<CardBehaviour> p_deck, BoardBehaviour p_board)
    {
        Player.SetBackpack(p_deck,p_board,null,0);

        EnterNode(DungeonNodeLib.GetNode("TestBattle"));
    }

    /// <summary>
    /// 开启一场测试战斗
    /// </summary>
    /// <param name="infoTest">战斗数据</param>
    public void EnterBattleForTest(EnterBattleInfoTest infoTest)
    {
        EnterBattleForTest(infoTest.p_Cards, infoTest.p_Board);
    }

    #endregion

    
}
