using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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
       // storagecards = ItemManager.Instance.GetItemByCategory(ItemCategory.Card);//初始化库存

        
        foreach(var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            if (item.Value > 0 || SaveSystem.Instance.getSave().PlayerHoldCards.ContainsKey(item.Key))
            {
                GameObject shopitem = GameObject.Instantiate(ShopItemPrefab, contentpanel.transform) as GameObject;
                string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
                CardItem cardItem = Resources.Load<CardItem>(FindCardItem);
                shopitem.name = cardItem.Name;
                shopitem.GetComponent<CardItemInventory>().carditem = cardItem;
                shopitem.GetComponent<CardItemInventory>().num = item.Value;
                shopitem.GetComponent<CardItemInventory>().showstate = 0;
                shopitem.GetComponent<CardItemInventory>().ResetNumText();
                GameObject newcard = GameObject.Instantiate(cardItem.cardBehaviour.gameObject, shopitem.transform) as GameObject;
                newcard.transform.SetSiblingIndex(0);
                //newcard.gameObject.GetComponent<TetrisAssembler>().enabled = false;
                newcard.gameObject.GetComponent<CardRotate>().enabled = false;
                newcard.gameObject.GetComponent<CardPosition>().enabled = false;
                newcard.gameObject.GetComponent<CardSetTarget>().enabled = false;
                //newcard.gameObject.GetComponent<CardUI>().enabled = false;
                Button button = shopitem.transform.GetChild(1).GetComponent<Button>();
                button.AddComponent<ButtonOfCard>();
            }
        }

        //playercards = PlayerHold.Instance.GetPlayerHoldCard();//初始化玩家持有
        //foreach(var item in playercards)
        //{
        //    string findcard = contentpanel.name + "/" + item.Key.Name;
        //    GameObject CardAdd = GameObject.Find(findcard);
        //    for(int i = 0; i < item.Value; i++)
        //    {
        //        cardInventoryUI.ShowItem(CardAdd);
        //    }
        //}

        //foreach (var item in SaveSystem.Instance.getSave().PlayerHoldCards)
        //{
        //    SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value);
        //}

        SerializableDictionary<string, int> pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().PlayerHoldCards);
        

        foreach (var item in pholdcards)
        {
            string findcard = contentpanel.name + "/" + item.Key;
            GameObject CardAdd = GameObject.Find(findcard);
            SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value);

            for (int i = 0; i < item.Value; i++)
            {
                cardInventoryUI.ShowItem(CardAdd);
            }

            //SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value);

            CardAdd.GetComponent<CardItemInventory>().ResetNumText();
        }

        
    }

    public void Return()
    {
        //Dictionary<string, int> newplayerhold = new Dictionary<string, int>();
        //for(int i = 0; i < PlayerHoldPanel.transform.childCount; i++)
        //{
        //    GameObject playercard = PlayerHoldPanel.transform.GetChild(i).gameObject;
        //    CardItem item = playercard.GetComponent<CardItemInventory>().carditem;
        //    if (newplayerhold.ContainsKey(item.Name))
        //    {
        //        newplayerhold[item.Name]++;
        //    }
        //    else
        //    {
        //        newplayerhold.Add(item.Name, 1);
        //    }
        //}

        //PlayerHold.Instance.ResetPlayerHold(newplayerhold);
        // SaveSystem.Instance.ResetPlayerHoldCards(newplayerhold);

        //SaveSystem.Instance.ResetPlayerHoldCards();
        //for (int i = 0; i < PlayerHoldPanel.transform.childCount; i++)
        //{
        //    GameObject playercard = PlayerHoldPanel.transform.GetChild(i).gameObject;
        //    CardItem item = playercard.GetComponent<CardItemInventory>().carditem;
        //    SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Name, 1);
        //}


        for (int i = PlayerHoldPanel.transform.childCount - 1; i >= 0; i--)
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
