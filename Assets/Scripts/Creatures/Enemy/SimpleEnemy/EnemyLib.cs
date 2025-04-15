using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyLib
{
    /// <summary>
    /// 所有存储
    /// </summary>
    public static Dictionary<string, EnemyBehaviour> prefabs = new();

    [RuntimeInitializeOnLoadMethod]
    static void LoadData()
    {
        EnemyBehaviour[] resources = Resources.LoadAll<EnemyBehaviour>("Prefabs/Enemies/ConcreteEnemies");
        foreach (EnemyBehaviour resource in resources)
        {
            prefabs.Add(resource.ID, resource);
        }
    }

    public static EnemyBehaviour GetEnemy(string id)
    {
        return MonoBehaviour.Instantiate(prefabs[id]);
    }
}
