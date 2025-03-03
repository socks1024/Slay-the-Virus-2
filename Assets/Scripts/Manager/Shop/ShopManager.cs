using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;



//�̵�
public class ShopManager : MonoBehaviour
{
    [SerializeField] 
    List<Item> StorageItems = new List<Item>();//���̵����Ķ����Ž�����

    public Dictionary<Item, int> storage = new Dictionary<Item, int>();
    private void Start()
    {
        InitStorage();
    }
    private void InitStorage()
    {
        foreach (var item in StorageItems)
        {
            AddToShopStorage(item, item.StorageAmount);
           // Debug.Log("������һ����Ʒ��");
        }
    }

    private void AddToShopStorage(Item item, int amount)//����������
    {
        if (storage.ContainsKey(item))
        {
            storage[item] += amount;
        }
        else
        {
            storage.Add(item, amount);
        }
    }

    private bool Purchase(Item item,int amount)//�ö�Ӧ��Դ������Ʒ��ӵ��ֿ�
    {
        int need = amount * item.price;
        if (amount > storage[item])
        {
            return false;
        }


        switch (item.resourceCatogory)
        {
            case ResourceCatogory.Nutrition:
                if (ResourceManager.Instance.nutrition > need)
                {
                    ResourceManager.Instance.RemoveNutrition(need);
                    storage[item] -= amount;
                    break;
                }
                else return false;
        }

        return ItemManager.Instance.AddItem(item, amount); 
    }

}




    
  

   



