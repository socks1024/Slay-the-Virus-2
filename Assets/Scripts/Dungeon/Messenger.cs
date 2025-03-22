using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Messenger
{
    public static EnterBattleInfoTest enterBattleInfoTest;
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
}
