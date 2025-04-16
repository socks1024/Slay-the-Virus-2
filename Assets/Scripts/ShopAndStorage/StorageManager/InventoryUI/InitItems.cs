using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitItems : MonoBehaviour
{
    private Dictionary<Item, int> storagecards = new Dictionary<Item, int>();
    private Dictionary<CardItem, int> playercards = new Dictionary<CardItem, int>();
    public GameObject contentpanel;
    public GameObject ShopItemPrefab;
    public GameObject PlayerHoldPanel;
    public CardInventoryUI cardInventoryUI;

    private void Start()
    {
        //ItemManager.Instance.AddItem(testcard, 10);//测试
        storagecards = ItemManager.Instance.GetItemByCategory(ItemCategory.Card);//初始化库存
        foreach(var item in storagecards)
        {
                 GameObject shopitem= GameObject.Instantiate(ShopItemPrefab, contentpanel.transform)as GameObject;
                 CardItem cardItem = (CardItem)item.Key;
                 shopitem.name = cardItem.Name;
                 shopitem.GetComponent<CardItemInventory>().carditem = cardItem;
                 shopitem.GetComponent<CardItemInventory>().num = item.Value;
                 shopitem.GetComponent<CardItemInventory>().ResetNumText();
                 GameObject newcard = GameObject.Instantiate(cardItem.cardBehaviour.gameObject, shopitem.transform) as GameObject;
                 newcard.transform.SetSiblingIndex(0);
                 Button button = shopitem.transform.GetChild(1).GetComponent<Button>();
                 button.AddComponent<ButtonOfCard>();
        }

        playercards = PlayerHold.Instance.GetPlayerHoldCard();//初始化玩家持有
        foreach(var item in playercards)
        {
            string findcard = contentpanel.name + "/" + item.Key.Name;
            GameObject CardAdd = GameObject.Find(findcard);
            for(int i = 0; i < item.Value; i++)
            {
                cardInventoryUI.ShowItem(CardAdd);
            }
        }
    }

    public void Return()
    {
        Dictionary<CardItem, int> newplayerhold = new Dictionary<CardItem, int>();
        for(int i = 0; i < PlayerHoldPanel.transform.childCount; i++)
        {
            GameObject playercard = PlayerHoldPanel.transform.GetChild(i).gameObject;
            CardItem item = playercard.GetComponent<CardItemInventory>().carditem;
            if (newplayerhold.ContainsKey(item))
            {
                newplayerhold[item]++;
            }
            else
            {
                newplayerhold.Add(item, 1);
            }
        }

        PlayerHold.Instance.ResetPlayerHold(newplayerhold);

        for(int i = PlayerHoldPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(PlayerHoldPanel.transform.GetChild(i).gameObject);
        }

        for(int i = contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentpanel.transform.GetChild(i).gameObject);
        }

        SceneManager.LoadScene("Base");
    }
}
