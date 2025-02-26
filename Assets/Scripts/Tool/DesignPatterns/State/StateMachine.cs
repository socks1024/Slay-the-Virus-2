using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    public State CurrentState { get; private set; }

    public event Action<State> StateChanged;

    StateMachine(State state, Action<State> action)
    {
        CurrentState = state;
        CurrentState.Enter(this);

        StateChanged = action;
    }

    protected void TransitionTo(State state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter(this);

        StateChanged?.Invoke(state);
    }




}
