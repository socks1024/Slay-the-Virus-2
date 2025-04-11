using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RelicLib
{
    /// <summary>
    /// 所有遗物存储
    /// </summary>
    public static Dictionary<string, RelicBehaviour> relicPrefabs = new();

    [RuntimeInitializeOnLoadMethod]
    static void LoadData()
    {
        RelicBehaviour[] resources = Resources.LoadAll<RelicBehaviour>("Prefabs/Relics/ConcreteRelic");
        foreach (RelicBehaviour resource in resources)
        {
            relicPrefabs.Add(resource.ID, resource);
        }
    }
}
