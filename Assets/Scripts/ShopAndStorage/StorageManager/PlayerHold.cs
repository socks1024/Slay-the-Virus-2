using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHold : MonoSingleton<PlayerHold>
{
    private Dictionary<Item, int> CarriedItems = new Dictionary<Item, int>();  //��ҳ�����Ʒ���б�
    private Dictionary<CardBehaviour,int> CarriedCards= new Dictionary<CardBehaviour, int>();//��ҳ��еĿ���
    private Dictionary<CardItem, int> CarriedCardItems = new Dictionary<CardItem, int>();
    private Item chessboard;//���ֻ��ѡһ�����̣������û����Ļ�...��

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

    public Dictionary<CardItem,int> GetPlayerHoldCard()
    {
        return CarriedCardItems;
    }

    public void ResetPlayerHold(Dictionary<CardItem, int> newcards)
    {
        CarriedCardItems.Clear();
        CarriedCardItems = newcards;
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

    public bool AddCard(CardItem card)
    {
        Debug.Log("card added!");
        if (CarriedCardItems.ContainsKey(card))
        {
            CarriedCardItems[card] += 1;
            return true;
        }
        else
        {
            CarriedCardItems.Add(card, 1);
            return true;
        }
    }

    /*public bool RemoveCard(CardBehaviour card)
    {
        if (CarriedCards.ContainsKey(card) && CarriedCards[card] > 0)
        {
            Debug.Log("card removed!");
            CarriedCards[card] -= 1;
            return true;
        }
        else return false;
    }*/

    public List<CardBehaviour> GetCardBehaviours()
    {
        
        List<CardBehaviour> cardBehaviours = new List<CardBehaviour>();
        foreach(var cardBehaviour in CarriedCards)
        {
           for(int i = 0; i < cardBehaviour.Value; i++)
            {
                cardBehaviours.Add(cardBehaviour.Key);
            }
        }

        return cardBehaviours;
    }
}
