using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DungeonEventInfo
{
    
}



public class BattleInfo : DungeonEventInfo
{
    /// <summary>
    /// 遭遇战中的敌人，按顺序排列
    /// </summary>
    List<EnemyBehaviour> enemies;

    /// <summary>
    /// 遭遇战的战利品
    /// </summary>
    List<Loot> loots; 
}

public class NormalEventInfo : DungeonEventInfo
{
    /// <summary>
    /// 事件的文本描述
    /// </summary>
    string text;
}

public class EvacuateInfo : NormalEventInfo
{
    public void Evacuate()
    {
        Debug.Log("Evacuate");
    }
}

public class ForkRoadInfo : NormalEventInfo
{
    /// <summary>
    /// 通向的后续几个事件
    /// </summary>
    public List<DungeonEventInfo> eventInfos;
}
