using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ʹ��UnityActionʵ�ֹ۲���ģʽ��������Ϊ�¼��н飬���¼�����ʱ֪ͨ����Listener
/// ÿ��EventTypeӵ�й̶��Ĳ������������ͣ����״δ�����ӦEventInfoʱ������
/// </summary>
public class EventCenter : BaseSingleton<EventCenter>
{
    private Dictionary<EventType,IEventInfo> eventDic = new();

    public void ClearAllEvents()
    {
        eventDic.Clear();
    }

    #region no argument

    public void AddEventListener(EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    public void TriggerEvent(EventType name)
    {
        Debug.Log("������������" + name + "������������");
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions?.Invoke();
        }
    }

    public void RemoveEventListener(EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    #endregion

    #region has argument

    public void AddEventListener<T>(EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    public void TriggerEvent<T>(EventType name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }

    public void RemoveEventListener<T>(EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    #endregion

    #region no argument has priority

    public void AddEventListener(EventType name, PriorityAction priorityAction) 
    {
        if (eventDic.ContainsKey(name))
        {
            //��Ϊ�Ǹ��ࣨIEventInfo��װ���ࣨEventInfo������asΪ���ࣨEventInfo������ʹ�����е�actions
            (eventDic[name] as PriorityEventInfo).Remove(priorityAction);
        }
        else
        {
            eventDic.Add(name, new PriorityEventInfo(priorityAction));
        }
    }

    public void EventTrigger_Priority(EventType name)
    {
        Debug.Log("������������" + name + "������������");
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as PriorityEventInfo).allActions.ForEach(val => { val.Action?.Invoke(); });
        }
    }

    public void RemoveEventListenerEventListener(EventType name, PriorityAction priorityAction) 
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as PriorityEventInfo).RemoveEvent(priorityAction);
        }
    }

    #endregion

    #region has argument and priority

    public void AddEventListener<T>(EventType name, PriorityAction<T> priorityAction) 
    {
        if (eventDic.ContainsKey(name))
        {
            //��Ϊ�Ǹ��ࣨIEventInfo��װ���ࣨEventInfo������asΪ���ࣨEventInfo������ʹ�����е�actions
            (eventDic[name] as PriorityEventInfo<T>).RemoveEvent(priorityAction);
        }
        else
        {
            eventDic.Add(name, new PriorityEventInfo<T>(priorityAction));
        }
    }

    public void EventTrigger_Priority<T>(EventType name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as PriorityEventInfo<T>).allActions.ForEach(val => { val.Action?.Invoke(info); });
        }
    }

    public void RemoveEventListenerEventListener<T>(EventType name, PriorityAction<T> priorityAction) 
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as PriorityEventInfo<T>).RemoveEvent(priorityAction);
        }
    }

    #endregion
}

public enum EventType
{
    //ս���¼�
    BATTLE_START,
    PLAYER_DEAD,
    SINGLE_ENEMY_KILLED,
    BATTLE_WIN,
    //�غ����¼�
    TURN_START,
    ACT_START,
    CARD_ACT_END,
    ENEMY_ACT_END,
}