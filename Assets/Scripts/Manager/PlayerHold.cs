using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerHold : MonoBehaviour
{
    public static PlayerHold Instance { get; private set; }
    private Dictionary<Item, int> CarriedItems = new Dictionary<Item, int>();  //玩家持有物品的列表
    private Item chessboard;//玩家只能选一个棋盘（如果我没理解错的话...）

    private void Awake()
    {
        Instance = this;
    }

    public bool TakeFromStorage(Item item,int amount)//从仓库中拿去一定量的物品到玩家持有的物品
    {
        int CurrentAmount = ItemManager.Instance.GetItemCount(item);
        if (CurrentAmount < amount)
        {
            return false;
        }

        if (ItemManager.Instance.RemoveItem(item, amount))
        {
            CarriedItems[item] += amount;
            return true;
        }

        return false;
    }

    public bool StoreToStorage(Item item,int amount)//把玩家手里的东西放回仓库
    {
        if (amount > CarriedItems[item])
        {
            return false;
        }
        CarriedItems[item] -= amount;
        return ItemManager.Instance.AddItem(item, amount);
    }

    public bool BoardChoice(Item item)//选棋盘
    {
        int CurrentAmount = ItemManager.Instance.GetItemCount(item);
        if (CurrentAmount <= 0)
            return false;

        chessboard = item;
        return true;
    }

    public int GetItemCount(Item item)//获取玩家手里某物品数量
    {
        return CarriedItems[item];
    }

}
