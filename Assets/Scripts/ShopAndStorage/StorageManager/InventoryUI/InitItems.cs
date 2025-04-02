using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InitItems : MonoBehaviour
{
    private Dictionary<Item, int> storagecards = new Dictionary<Item, int>();
    public GameObject contentpanel;
    public GameObject ShopItemPrefab;
    public CardItem testcard;
    public CardInventoryUI inventoryUI;

    private void Start()
    {
        ItemManager.Instance.AddItem(testcard, 10);//≤‚ ‘
        storagecards = ItemManager.Instance.GetItemByCategory(ItemCategory.Card);
        foreach(var item in storagecards)
        {
            for(int i = 0; i < storagecards[item.Key]; i++)
            {
                 GameObject shopitem= GameObject.Instantiate(ShopItemPrefab, contentpanel.transform)as GameObject;
                 CardItem cardItem = (CardItem)item.Key;
                 shopitem.GetComponent<CardItemInventory>().carditem = cardItem;
                 GameObject newcard = GameObject.Instantiate(cardItem.cardBehaviour.gameObject, shopitem.transform) as GameObject;
                 newcard.transform.SetSiblingIndex(0);
                 Button button = shopitem.transform.GetChild(1).GetComponent<Button>();
                 button.AddComponent<ButtonOfCard>();
            }
        }
    }
}
