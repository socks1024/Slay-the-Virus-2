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
        bool hasItem = inventory.TryGetValue(item, out int currentAmount);

        int maxAdd = hasItem ? (item.MaxAmount - currentAmount) : item.MaxAmount;

        int Add = Mathf.Min(maxAdd, amount);
        if (Add <= 0)   //��Ʒ����������
        {
            return false;
        }

        inventory[item] = (hasItem ? currentAmount : 0) + Add;
        return true;

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

    
}
