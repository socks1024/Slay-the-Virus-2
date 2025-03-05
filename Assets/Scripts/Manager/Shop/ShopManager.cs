using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;



//商店
public class ShopManager : MonoBehaviour
{
    [SerializeField] 
    private List<Item> StorageItems = new List<Item>();//把商店卖的东西放进这里
    [SerializeField]
    private List<Item> ExtractItems = new List<Item>();//抽奖池物品放这里

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
           // Debug.Log("加载了一个商品！");
        }
    }

    private void InitExtract()
    {
        foreach (var item in ExtractItems)
        {
            AddToExtract(item, item.weight);
        }
    }

    private Item Extract()//抽奖！
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

    private void AddToShopStorage(Item item, int amount)//往库存里添加
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

    private void AddToExtract(Item item, int weight)//往抽奖池里添加一定权重的某物
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


    private bool Purchase(Item item,int amount)//用对应资源购买物品添加到仓库
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

    private bool SellItem(Item item,int amount)//从仓库里卖东西
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




    
  

   



