using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;



//商店
public class ShopManager : MonoBehaviour
{
    [SerializeField] 
    List<Item> StorageItems = new List<Item>();//把商店卖的东西放进这里

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
           // Debug.Log("加载了一个商品！");
        }
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

}




    
  

   



