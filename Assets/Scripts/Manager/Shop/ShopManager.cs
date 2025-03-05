using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;



//�̵�
public class ShopManager : MonoBehaviour
{
    [SerializeField] 
    private List<Item> StorageItems = new List<Item>();//���̵����Ķ����Ž�����
    [SerializeField]
    private List<Item> ExtractItems = new List<Item>();//�齱����Ʒ������

    public Dictionary<Item, int> storage = new Dictionary<Item, int>();
    public Dictionary<Item, int> extract = new Dictionary<Item, int>();
    private void Start()
    {
        InitStorage();
        InitExtract();
        //for (int i = 0; i < 10; i++) Extract();
    }
    private void InitStorage()
    {
        foreach (var item in StorageItems)
        {
            AddToShopStorage(item, item.StorageAmount);
           // Debug.Log("������һ����Ʒ��");
        }
    }

    private void InitExtract()
    {
        foreach (var item in ExtractItems)
        {
            AddToExtract(item, item.weight);
        }
    }

    private Item Extract()//�齱��
    {
        int totalweight = 0;
        foreach(int weight in extract.Values)
        {
            totalweight += weight;
        }
        int rand = Random.Range(0, totalweight);
        int currentnum = 0;
        foreach (var item in extract)
        {
            currentnum += item.Value;
            if (rand < currentnum)
            {
                //Debug.Log(item.Key.Name);
                return item.Key;
            }
        }
        //Debug.Log(extract.Keys.Last().Name);
        return extract.Keys.Last();
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

    private void AddToExtract(Item item, int weight)//���齱�������һ��Ȩ�ص�ĳ��
    {
        if (extract.ContainsKey(item))
        {
            extract[item] += weight;
        }
        else
        {
            extract.Add(item, weight);
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

    private bool SellItem(Item item,int amount)//�Ӳֿ���������
    {
        if (ItemManager.Instance.GetItemCount(item) < amount)
        {
            return false;
        }

        int earning = amount * item.sellprice;

        switch (item.resourceCatogory)
        {
            case ResourceCatogory.Nutrition:
                ResourceManager.Instance.AddNutrition(earning);
                break;
        }

        return ItemManager.Instance.RemoveItem(item, amount);


    }

}




    
  

   



