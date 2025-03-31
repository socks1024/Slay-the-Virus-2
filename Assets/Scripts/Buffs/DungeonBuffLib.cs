using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DungeonBuffLib
{
    /// <summary>
    /// buff总存储
    /// </summary>
    public static Dictionary<string, BuffBehaviour> buffPrefabs = new Dictionary<string, BuffBehaviour>();

    /// <summary>
    /// 获取某个特定的buff
    /// </summary>
    /// <param name="name">buff名字</param>
    /// <param name="amount">buff数值</param>
    /// <returns></returns>
    public static BuffBehaviour GetBuff(string name, int amount)
    {
        BuffBehaviour buff = MonoBehaviour.Instantiate(buffPrefabs[name]);
        buff.Amount = amount;
        return buff;
    }

    [InitializeOnLoadMethod]
    public static void LoadBuffs()
    {
        LoadBuff("DefaultBuff");
    }

    /// <summary>
    /// 加载Buff
    /// </summary>
    /// <param name="name">Buff名称</param>
    static void LoadBuff(string name)
    {
        BuffBehaviour buff = Resources.Load<BuffBehaviour>("Prefabs/Buffs/ConcreteBuffs/" + name);
        buffPrefabs.Add(buff.ID, buff);
    }
}
