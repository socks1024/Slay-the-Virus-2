using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    [SerializeField] BoardBehaviour boardPrefab;

    [SerializeField] CanvasGroup actButtonRoot;

    /// <summary>
    /// 各种UI元素在画面外的临时储存点
    /// </summary>
    public RectTransform storage;

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
    void EnterBattle(DungeonNode battleInfo)
    {
        RightBG.DOMove(rightBGBattlePos, BGMoveTime).OnComplete(() => {
            battleManager.gameObject.SetActive(true);
            battleManager.InitializeEncounter(battleInfo);
            EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
        });
        actButtonRoot.DOFade(1, BGMoveTime);
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
    void EnterEvent(DungeonNode eventInfo)
    {
        eventManager.SetEvent(eventInfo);
        eventManager.gameObject.SetActive(true);
    }

    #endregion

    #region rest management

    /// <summary>
    /// 休息点处理脚本
    /// </summary>
    public RestManager restManager;

    /// <summary>
    /// 触发休息点
    /// </summary>
    /// <param name="restNode">休息点</param>
    void EnterRest(RestNode restNode)
    {
        restManager.SetRest(restNode);
        restManager.gameObject.SetActive(true);
    }  

    #endregion

    #region dungeon UI

    [Header("UI")]
    public Transform LeftBG;
    public Transform RightBG;
    public Transform RightBGBattleTransform;
    
    /// <summary>
    /// 右侧背景的战斗时位置
    /// </summary>
    Vector3 rightBGBattlePos{ get{ return RightBGBattleTransform.position; }}

    /// <summary>
    /// 背景移动花费的时间
    /// </summary>
    public float BGMoveTime = 0.5f;

    /// <summary>
    /// 将背景移动回去
    /// </summary>
    /// <param name="onComplete">移动结束时触发的回调</param>
    public void RightBGReturnBack(UnityAction onComplete)
    {
        RightBG.DOMove(Vector3.zero, BGMoveTime).OnComplete(onComplete.Invoke);
        actButtonRoot.DOFade(0, BGMoveTime);
    }

    #endregion

    #region map
    
    MapGenerator mapGenerator;

    /// <summary>
    /// 当前进行到的地牢节点
    /// </summary>
    public DungeonNode CurrNode
    {
        get { return currNode;}
        set 
        {
            if (currNode == value)
            {
                return;
            }

            if (currNode != null)
            {
                switch (currNode.nodeType)
                {
                    case DungeonNodeType.BATTLE:
                        battleManager.gameObject.SetActive(false);
                        break;
                    case DungeonNodeType.EVENT:
                        eventManager.gameObject.SetActive(false);
                        break;
                    case DungeonNodeType.REST:
                        restManager.gameObject.SetActive(false);
                        break;
                }
            }

            switch (value.nodeType)
            {
                case DungeonNodeType.BATTLE:
                    EnterBattle(value);
                    break;
                case DungeonNodeType.EVENT:
                    EnterEvent(value);
                    break;
                case DungeonNodeType.REST:
                    EnterRest(value as RestNode);
                    break;
            }

            currNode = value;
        }
    }
    DungeonNode currNode;

    /// <summary>
    /// 进入下一个节点
    /// </summary>
    public void EnterNode(DungeonNode node)
    {
        CurrNode = node;

        node.visited = true;
    }

    #endregion

    #region leave dungeon

    void LeaveDungeon()
    {
        Messenger.leaveBattleInfoTest.p_Relics = Player.p_Relics;
        Messenger.leaveBattleInfoTest.p_Board = Player.p_Board;
        Messenger.leaveBattleInfoTest.p_Cards = Player.p_Deck;
        Messenger.leaveBattleInfoTest.nutrition = Player.Nutrition;

        AudioManager.Instance.StopMusic();

        SceneManager.LoadScene("Base");
    }

    public void WinLeaveDungeon()
    {
        switch (mission.DungeonName)
        {
            case "BossFight1":
                SaveSystem.Instance.LevelClear(1);
                break;
            case "BossFight2":
                SaveSystem.Instance.LevelClear(2);
                break;
            case "BossFight3":
                SaveSystem.Instance.LevelClear(3);
                break;
            case "BossFight4":
                SaveSystem.Instance.LevelClear(4);
                break;
            case "BossFight5":
                SaveSystem.Instance.LevelClear(5);
                break;
            case "BossFight6":
                SaveSystem.Instance.LevelClear(6);
                break;
        }

        LeaveDungeon();
    }

    public void LoseLeaveDungeon()
    {
        LeaveDungeon();
    }

    public void SettingLeaveDungeon()
    {
        LeaveDungeon();
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        RightBG.gameObject.SetActive(true);
        RightBG.transform.position = new Vector3(0, 0, 10);
        RightBGBattleTransform.gameObject.SetActive(false);

        mapGenerator = GetComponent<MapGenerator>();
        battleManager.gameObject.SetActive(false);
        eventManager.gameObject.SetActive(false);
        restManager.gameObject.SetActive(false);

        // StartAdventure(Messenger.enterDungeonInfo);
    }

    DungeonMissionData mission;

    public void StartAdventure(EnterDungeonInfo enterDungeonInfo)
    {
        mission = enterDungeonInfo.missionData;

        AudioManager.Instance.PlayMusic(mission.ID);

        Player.SetBackpack(enterDungeonInfo.p_Cards, boardPrefab, enterDungeonInfo.p_Relics, 0);

        mapGenerator.GenerateMap(enterDungeonInfo);

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

    public void TestLeaveBattleScene()
    {
        WinLeaveDungeon();
    }
}


