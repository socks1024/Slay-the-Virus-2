using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHold : MonoBehaviour
{
    public static PlayerHold Instance { get; private set; }
    private Dictionary<Item, int> CarriedItems = new Dictionary<Item, int>();  //��ҳ�����Ʒ���б�
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

    public bool BoardChoice(Item item)//ѡ����
    {
        int CurrentAmount = ItemManager.Instance.GetItemCount(item);
        if (CurrentAmount <= 0)
            return false;

        chessboard = item;
        return true;
    }

}
