using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : MonoBehaviour
{
    public void Act()
    {
        EventCenter.Instance.TriggerEvent(EventType.TURN_END);
    }
}
