using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerHold : MonoSingleton<PlayerHold>
{
    public static PlayerHold Instance { get; private set; }
    private Dictionary<Item, int> CarriedItems = new Dictionary<Item, int>();  //��ҳ�����Ʒ���б�
    private Dictionary<CardData,int>CarriedCards= new Dictionary<CardData, int>();//��ҳ��еĿ���
    private Item chessboard;//���ֻ��ѡһ�����̣������û����Ļ�...��

    private void Awake()
    {
        Instance = this;
    }

    public bool TakeFromStorage(Item item,int amount)//�Ӳֿ�����ȥһ��������Ʒ����ҳ��е���Ʒ
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

    public bool StoreToStorage(Item item,int amount)//���������Ķ����Żزֿ�
    {
        if (amount > CarriedItems[item])
        {
            return false;
        }
        CarriedItems[item] -= amount;
        return ItemManager.Instance.AddItem(item, amount);
    }

    public bool BoardChoice(Item item)//ѡ����
    {
        int CurrentAmount = ItemManager.Instance.GetItemCount(item);
        if (CurrentAmount <= 0)
            return false;

        chessboard = item;
        return true;
    }

    public int GetItemCount(Item item)//��ȡ�������ĳ��Ʒ����
    {
        return CarriedItems[item];
    }

    public bool AddCard(CardData card)
    {
        Debug.Log("card added!");
        if (CarriedCards.ContainsKey(card))
        {
            CarriedCards[card] += 1;
            return true;
        }
        else
        {
            CarriedCards.Add(card, 1);
            return true;
        }
    }

    public bool RemoveCard(CardData card)
    {
        if (CarriedCards.ContainsKey(card) && CarriedCards[card] > 0)
        {
            Debug.Log("card removed!");
            CarriedCards[card] -= 1;
            return true;
        }
        else return false;
    }

}
