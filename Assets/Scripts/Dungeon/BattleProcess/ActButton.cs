using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : MonoBehaviour
{
    public void Act()
    {
        DungeonManager.Instance.battleManager.TriggerCardActRelics();
        DungeonManager.Instance.battleManager.enemyGroup.EnemyActBeforCardAct();
        EventCenter.Instance.TriggerEvent(EventType.ACT_START);
    }
}
