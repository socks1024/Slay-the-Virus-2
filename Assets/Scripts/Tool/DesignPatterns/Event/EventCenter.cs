using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 使用UnityAction实现观察者模式，该类作为事件中介，在事件触发时通知所有Listener
/// 每种EventType拥有固定的参数个数和类型，在首次创建对应EventInfo时被分配
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
            (eventDic[name] as EventInfo).actions?.AddListener(action);
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    public void TriggerEvent(EventType name)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions?.Invoke();
        }
    }

    public void RemoveEventListener(EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions?.RemoveListener(action);
        }
    }

    #endregion

    #region has argument

    public void AddEventListener<T>(EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions?.AddListener(action);
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
            (eventDic[name] as EventInfo<T>).actions?.RemoveListener(action);
        }
    }

    #endregion

}

public enum EventType
{
    PLAYER_DEAD,
}