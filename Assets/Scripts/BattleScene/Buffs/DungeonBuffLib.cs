using System.Collections;
using System.Collections.Generic;
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

    [RuntimeInitializeOnLoadMethod]
    public static void LoadBuffs()
    {
        LoadBuff();
    }

    /// <summary>
    /// 加载Buff
    /// </summary>
    static void LoadBuff()
    {
        BuffBehaviour[] buffs = Resources.LoadAll<BuffBehaviour>("Prefabs/Buffs/ConcreteBuffs");
        foreach (BuffBehaviour buff in buffs)
        {
            buffPrefabs.Add(buff.ID, buff);
        }
    }
}
