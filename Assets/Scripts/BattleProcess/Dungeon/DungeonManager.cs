using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    //进入副本的玩家的数据：携带的卡牌等
    //副本的数据：地图的生成逻辑是什么，包含哪些事件，可能产生哪些遭遇战，最终BOSS是什么

    //玩家：赋值玩家的deck，board，items，初始化生命值和变量

    //地图生成算法（单独一个类？)

    //地图事件（数据Info类 + 工厂模式？由地图生成算法产出，包括事件数据类和遭遇战数据类等等）






    /// <summary>
    /// 目前已经通过的房间数
    /// </summary>
    public int passedRoomCount = 0;

    public void EnterDungeon()
    {

    }

    public void ExitDungeon()
    {

    }

    DungeonEventInfo GetDungeonEvent()
    {
        DungeonEventInfo dungeonEvent = null;
        return dungeonEvent;
    }
}
