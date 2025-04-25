using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;
using UnityEngine.UI;

public class ActButton : MonoSingletonDestroyOnLoad<ActButton>
{
    [SerializeField] float WaitTime;

    Button button;

    protected override void Awake()
    {
        base.Awake();
        button = GetComponent<Button>();

        EventCenter.Instance.AddEventListener(EventType.TURN_START, SetEnableButton);
        EventCenter.Instance.AddEventListener(EventType.ACT_START, SetDisableButton);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, SetEnableButton);
        EventCenter.Instance.RemoveEventListener(EventType.ACT_START, SetDisableButton);
    }

    public void Act()
    {
        DungeonManager.Instance.battleManager.TriggerCardActRelics();
        DungeonManager.Instance.battleManager.enemyGroup.EnemyActBeforCardAct();
        EventCenter.Instance.TriggerEvent(EventType.ACT_START);
    }

    public void SetDisableButton()
    {
        button.interactable = false;
    }

    public void SetEnableButton()
    {
        button.interactable = true;
    }
}
