using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingletonDestroyOnLoad<T> : MonoBehaviour where T : MonoSingletonDestroyOnLoad<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            Object @lock = MonoSingletonDestroyOnLoad<T>.m_lock;
            lock (@lock)
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();

                    if (instance == null )
                    {
                        // instance = new GameObject(typeof(T) + "SingletonManager").AddComponent<T>();
                        // instance.Init();
                        Debug.LogError("No " + typeof(T) + " in this scene, please create the game object first.");
                    }

                    //DontDestroyOnLoad(instance.gameObject);
                }
            }
            
            return instance;
        }
    }

    private static Object m_lock = new Object();

    public static bool IsValid => MonoSingletonDestroyOnLoad<T>.instance != null;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this as T;
        //DontDestroyOnLoad(instance.gameObject);
    }

    public static void InitSubsystemRegistration()
    {
        MonoSingletonDestroyOnLoad<T>.instance = default(T);
        MonoSingletonDestroyOnLoad<T>.m_lock = new Object();
    }

    protected void OnDestroy()
    {
        MonoSingletonDestroyOnLoad<T>.instance = default(T);
    }

    protected virtual void Init()
    {

    }
}
