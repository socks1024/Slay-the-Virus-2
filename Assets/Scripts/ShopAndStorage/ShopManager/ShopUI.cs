using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Text resource;
    public Text price;
    
    private Item item;

    public void Start()
    {
       resource.text = ResourceManager.Instance.nutrition().ToString();
    }

    public void GetOneItem(Item oneitem)
    {
        Debug.Log("price change!");
        item = oneitem;
        price.text = item.price.ToString();
    }

    public void RemoveOneItem()
    {
        item = null;
        price.text = null;
    }

    public void trybuysth()
    {
        ShopManager.Instance.Purchase(item, 1);
        resource.text = ResourceManager.Instance.nutrition().ToString();
    }
}
