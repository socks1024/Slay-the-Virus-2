using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ֿ�ľ���ʵ��
public class ItemManager : MonoBehaviour
{
   public static ItemManager Instance { get; private set; }

    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    private void Awake()
    {
        Instance = this;
    }

    public bool AddItem(Item item,int amount)//���
    {
        /* bool hasItem = inventory.TryGetValue(item, out int currentAmount);

         int maxAdd = hasItem ? (item.MaxAmount - currentAmount) : item.MaxAmount;

         int Add = Mathf.Min(maxAdd, amount);
         if (Add <= 0)   //��Ʒ����������
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

    public bool RemoveItem(Item item,int amount)//�Ƴ�
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

    public int GetItemCount(Item item)//��ȡ����
    {
        return inventory.TryGetValue(item, out int amount) ? amount : 0;
    }

    public Dictionary<Item,int> GetItemByCategory(ItemCategory category)//����һ����Ʒ
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

    public int SumOfCatogory(ItemCategory category)//����һ����Ʒ������
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
