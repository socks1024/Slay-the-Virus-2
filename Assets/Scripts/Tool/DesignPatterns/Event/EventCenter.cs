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

}

public enum EventType
{
    //ս���¼�
    PLAYER_DEAD,
    SINGLE_ENEMY_KILLED,
    BATTLE_WIN,
    //�غ����¼�
    TURN_START,
    TURN_END,
    CARD_ACT_END,
    ENEMY_ACT_END,
}