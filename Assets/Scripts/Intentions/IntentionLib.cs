using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntentionLib
{
    /// <summary>
    /// 所有遗物存储
    /// </summary>
    public static Dictionary<string, IntentionBehaviour> prefabs = new();

    [RuntimeInitializeOnLoadMethod]
    static void LoadData()
    {
        IntentionBehaviour[] resources = Resources.LoadAll<IntentionBehaviour>("Prefabs/Intention/ConcreteIntentions");
        foreach (IntentionBehaviour resource in resources)
        {
            prefabs.Add(resource.ID, resource);
        }
    }
}
