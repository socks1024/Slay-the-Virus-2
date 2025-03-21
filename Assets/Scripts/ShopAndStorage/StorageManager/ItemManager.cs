using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仓库的具体实现
public class ItemManager : MonoBehaviour
{
   public static ItemManager Instance { get; private set; }

    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    private void Awake()
    {
        Instance = this;
    }

    public bool AddItem(Item item,int amount)//添加
    {
        /* bool hasItem = inventory.TryGetValue(item, out int currentAmount);

         int maxAdd = hasItem ? (item.MaxAmount - currentAmount) : item.MaxAmount;

         int Add = Mathf.Min(maxAdd, amount);
         if (Add <= 0)   //物品数量达上限
         {
             return false;
         }

         inventory[item] = (hasItem ? currentAmount : 0) + Add;
         return true;*/

        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
            return true;
        }
        else
        {
            inventory.Add(item, amount);
            return true;
        }
       
    }

    public bool RemoveItem(Item item,int amount)//移除
    {
        if(!inventory.TryGetValue(item, out int currentAmount) || currentAmount < amount)
        {
            return false;
        }

        inventory[item] -= amount;

        if (inventory[item] < 0)
        {
            inventory.Remove(item);
        }

        return true;
    }

    public int GetItemCount(Item item)//获取数量
    {
        return inventory.TryGetValue(item, out int amount) ? amount : 0;
    }

    public Dictionary<Item,int> GetItemByCategory(ItemCategory category)//返回一类物品
    {
        Dictionary<Item, int> ItemOfTheCatogory = new Dictionary<Item, int>();
       foreach (var kvp in inventory)
        {
            if (kvp.Key.Category == category)
            {
                ItemOfTheCatogory[kvp.Key] = kvp.Value;
            }
        }
        return ItemOfTheCatogory;
    }

    public int SumOfCatogory(ItemCategory category)//返回一类物品的总数
    {
        int sum = 0;
        foreach (var kvp in inventory)
        {
            if (kvp.Key.Category == category)
            {
                sum += kvp.Value;
            }
        }
        return sum;
    }
}
