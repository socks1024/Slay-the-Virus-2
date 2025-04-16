using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootInfo", menuName = "ScriptableObject/LootInfo", order = 0)]
public class LootInfo : ScriptableObject
{
    /// <summary>
    /// 战利品中所有可能出现的卡包
    /// </summary>
    public List<CardPack> cardPackPool;

    /// <summary>
    /// 战利品中所有可能出现的道具
    /// </summary>
    public List<RelicBehaviour> relicPool;

    /// <summary>
    /// 胜利获得金钱的数量
    /// </summary>
    public int money;

    /// <summary>
    /// 从卡包池中抽取一定数量的卡包
    /// </summary>
    /// <param name="count">抽取卡包的数量</param>
    /// <returns>抽到的卡包</returns>
    public List<CardPack> RandomGetCardPacks(int count)
    {
        List<CardPack> packs = new List<CardPack>();
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, cardPackPool.Count);
            packs.Add(cardPackPool[index]);
        }
        return packs;
    }

    /// <summary>
    /// 从道具池中抽取一定数量的道具
    /// </summary>
    /// <param name="count">抽取道具的数量</param>
    /// <returns>抽到的道具</returns>
    public List<RelicBehaviour> RandomGetItems(int count)
    {
        List<RelicBehaviour> items = new List<RelicBehaviour>();
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, relicPool.Count);
            items.Add(relicPool[index]);
        }
        return items;
    }
}
