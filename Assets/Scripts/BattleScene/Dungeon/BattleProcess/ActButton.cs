using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;
using UnityEngine.UI;

public class ActButton : MonoSingletonDestroyOnLoad<ActButton>
{
    [SerializeField] float WaitTime;

    public Button button;

    protected override void Awake()
    {
        base.Awake();
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
        EventCenter.Instance.AddEventListener(EventType.TURN_START, SetEnableButton);
    }

    public void SetEnableButton()
    {
        button.interactable = true;
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, SetEnableButton);
    }
}
