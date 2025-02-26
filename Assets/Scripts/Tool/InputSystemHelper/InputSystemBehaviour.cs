using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputSystemBehaviour<T> : MonoBehaviour where T : IInputActionCollection, new()
{
    protected T inputAction;

    protected void Awake()
    {
        inputAction = new T();
    }

    protected void OnEnable()
    {
        inputAction.Enable();
        AddInputDelegate();
    }

    protected void OnDisable()
    {
        inputAction.Disable();
        RemoveInputDelegate();
    }

    protected abstract void AddInputDelegate();

    protected abstract void RemoveInputDelegate();
}
