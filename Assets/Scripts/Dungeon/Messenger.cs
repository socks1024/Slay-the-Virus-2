using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Messenger
{
    public static EnterDungeonInfo enterDungeonInfo;

    public static EnterBattleInfoTest enterBattleInfoTest;

    public static LeaveBattleInfoTest leaveBattleInfoTest;
}

public struct EnterBattleInfoTest
{
    /// <summary>
    /// 卡组
    /// </summary>
    public List<CardBehaviour> p_Cards;

    /// <summary>
    /// 使用的棋盘
    /// </summary>
    public BoardBehaviour p_Board;

    /// <summary>
    /// 出现的敌人
    /// </summary>
    public List<EnemyBehaviour> p_Enemies;

    /// <summary>
    /// 道具/遗物
    /// </summary>
    public List<RelicBehaviour> p_Relics;
}

public struct LeaveBattleInfoTest
{
    /// <summary>
    /// 卡组
    /// </summary>
    public List<CardBehaviour> p_Cards;

    /// <summary>
    /// 使用的棋盘
    /// </summary>
    public BoardBehaviour p_Board;

    /// <summary>
    /// 带回的营养
    /// </summary>
    public int nutrition;

    /// <summary>
    /// 道具/遗物
    /// </summary>
    public List<RelicBehaviour> p_Relics;
}

public struct EnterDungeonInfo
{
    /// <summary>
    /// 卡组
    /// </summary>
    public List<CardBehaviour> p_Cards;

    /// <summary>
    /// 道具/遗物
    /// </summary>
    public List<RelicBehaviour> p_Relics;

    /// <summary>
    /// 任务数据
    /// </summary>
    public DungeonMissionData missionData;
}
