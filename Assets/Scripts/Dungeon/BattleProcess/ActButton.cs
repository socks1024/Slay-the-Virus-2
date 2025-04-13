using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : MonoBehaviour
{
    public void Act()
    {
        DungeonManager.Instance.battleManager.TriggerCardActRelics();
        EventCenter.Instance.TriggerEvent(EventType.ACT_START);
    }
}
