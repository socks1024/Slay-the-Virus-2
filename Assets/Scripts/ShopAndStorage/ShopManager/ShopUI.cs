using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Text resource;
    public Text price;
    
    private List<Item> items = new List<Item>();
    private int sumprice;

    public void Start()
    {
       resource.text = ResourceManager.Instance.nutrition().ToString();
    }

    public void GetOneItem(Item oneitem)
    {
        Debug.Log("price change!");
        items.Add(oneitem);
        UpdatePrice();
        price.text = sumprice.ToString();
        resourcetextcolor();
    }

    private void UpdatePrice()
    {
        sumprice = 0;
       foreach(Item oneitem in items)
        {
            sumprice += oneitem.price;
        }
    }

    public void RemoveOneItem(Item item)
    {
        items.Remove(item);
        UpdatePrice();
        price.text = sumprice.ToString();
        resourcetextcolor();
        if (items.Count == 0)
            resource.color = Color.black;
    }

    public void trybuysth()
    {
        if (ResourceManager.Instance.nutrition()<sumprice)
        {
            return;
        }
        foreach (Item item in items)
        {
            ShopManager.Instance.Purchase(item, 1);
        }
        resource.text = ResourceManager.Instance.nutrition().ToString();
        resourcetextcolor();
    }

    private void resourcetextcolor()
    {
        if (ResourceManager.Instance.nutrition() >= sumprice)
            resource.color = Color.green;
        else resource.color = Color.red;
    }
}
