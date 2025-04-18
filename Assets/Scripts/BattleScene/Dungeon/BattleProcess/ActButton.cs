using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;
using UnityEngine.UI;

public class ActButton : MonoBehaviour
{
    [SerializeField] float WaitTime;

    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Act()
    {
        DungeonManager.Instance.battleManager.TriggerCardActRelics();
        DungeonManager.Instance.battleManager.enemyGroup.EnemyActBeforCardAct();
        EventCenter.Instance.TriggerEvent(EventType.ACT_START);

        SetDisableButton();
    }

    public void SetDisableButton()
    {
        button.interactable = false;
        // TimersManager.SetTimer("ActButtonWait", WaitTime, () => button.interactable = true );
        EventCenter.Instance.AddEventListener(EventType.TURN_START, SetEnableButton);
    }

    public void SetEnableButton()
    {
        button.interactable = true;
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, SetEnableButton);
    }
}
