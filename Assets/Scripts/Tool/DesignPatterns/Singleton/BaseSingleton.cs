using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��MonoScript��������
/// </summary>
/// <typeparam name="T">�̳��������</typeparam>
public abstract class BaseSingleton<T> where T : class, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }

            return instance;
        }
    }

    protected static readonly object instanceLock = new object();
    
}
