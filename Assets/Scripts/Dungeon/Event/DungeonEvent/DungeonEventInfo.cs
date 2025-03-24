using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct DungeonBattleInfo
{
    public List<EnemyBehaviour> p_Enemies;

    public UnityAction<int> OnAllActEndCallback;
}

public struct DungeonEventInfo
{
    public string eventDescription;

    public List<EventChoice> choices;
}
