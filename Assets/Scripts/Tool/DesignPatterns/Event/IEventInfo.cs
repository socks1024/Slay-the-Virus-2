using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

public class EventInfo : IEventInfo
{
    public UnityEvent actions;

    public EventInfo(UnityAction action)
    {
        this.actions.AddListener(action);
    }
}

public class EventInfo<T> : IEventInfo
{
    public UnityEvent<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        this.actions.AddListener(action);
    }
}