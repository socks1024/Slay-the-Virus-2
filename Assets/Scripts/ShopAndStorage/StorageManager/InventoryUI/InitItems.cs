using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
       
        SerializableDictionary<string, int> pholdcards=new SerializableDictionary<string, int>();
        switch (SaveSystem.Instance.getSave().CardPresetIndex)
        {
            case 1:
                pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().CardPreset1);
                break;
            case 2:
                pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().CardPreset2);
                break;
            case 3:
                pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().CardPreset3);
                break;
        }
        
        foreach (var item in pholdcards)
        {
            SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value,SaveSystem.Instance.getSave().CardPresetIndex);
        }

            foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
           {
                if (SaveSystem.Instance.getSave().PlayerGotCards.Contains(item.Key))
                {
                string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
                CardItem cardItem = Resources.Load<CardItem>(FindCardItem);
                Initcard(cardItem, item.Value);
                }
            }

        

        foreach (var item in pholdcards)
        {
            string findcard = contentpanel.name + "/" + item.Key;
            GameObject CardAdd = GameObject.Find(findcard);
            //SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value);

            for (int i = 0; i < item.Value; i++)
            {
                cardInventoryUI.ShowItem(CardAdd);
            }

            //SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value);

            CardAdd.GetComponent<CardItemInventory>().ResetNumText();
        }



        if (SaveSystem.Instance.getSave().TutorialClear[1] == false)
        {
            DialogueManager.Instance.ShowDialoguePanel().AddDialogueEvent(DialogueManager.Instance.loader, "camp").ShowNextDialogueEvent();
            SaveSystem.Instance.SetTutorialClear(1);
        }
    }

    public void ReInitPlayerHoldCards(int index)
    {
        SaveSystem.Instance.ChangePreset(index);


        for (int i = PlayerHoldPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(PlayerHoldPanel.transform.GetChild(i).gameObject);
        }

        SerializableDictionary<string, int> pholdcards = new SerializableDictionary<string, int>();
        switch (index)
        {
            case 1:
                pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().CardPreset1);
                break;
            case 2:
                pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().CardPreset2);
                break;
            case 3:
                pholdcards = new SerializableDictionary<string, int>(SaveSystem.Instance.getSave().CardPreset3);
                break;
        }

        foreach (var item in pholdcards)
        {
            SaveSystem.Instance.AddPlayerHoldCardsFromInventory(item.Key, -item.Value,index);
            string findcard = contentpanel.name + "/" + item.Key;
            GameObject CardAdd = GameObject.Find(findcard);
            if (CardAdd != null)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    cardInventoryUI.ShowItem(CardAdd,index);
                }
            }
            CardAdd.GetComponent<CardItemInventory>().ResetNumText();
        }
    }

   

    public void ReInitCardsByPack(bool[] bools)
    {
        for (int i = contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentpanel.transform.GetChild(i).gameObject);
        }


        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            if (SaveSystem.Instance.getSave().PlayerGotCards.Contains(item.Key))
            {
                string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
                CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

                switch (cardItem.cardBehaviour.Pack)
                {
                    case CardPack.BASIC_NORMAL:
                    case CardPack.BASIC_STATE:
                    case CardPack.BASIC_SUPPORT:
                        if (bools[0] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.AMMO:
                        if (bools[3] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.SIDE_EFFECT:
                        if (bools[4] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.SPECIAL_FORCES:
                        if (bools[5] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.LOGISTICS:
                        if (bools[6] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.SHELTER:
                        if (bools[7] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.WAR_MACHINE:
                        if (bools[8] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.TRAINING:
                        if (bools[2] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardPack.MEDICAL:
                        if (bools[1] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                }
            }
        }

    }

    public void ReInitCardsByRarity(bool[] bools)
    {
        for (int i = contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentpanel.transform.GetChild(i).gameObject);
        }


        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            if (SaveSystem.Instance.getSave().PlayerGotCards.Contains(item.Key))
            {
                string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
                CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

                switch (cardItem.cardBehaviour.RarityType)
                {
                    case CardRarityType.COMMON:
                        if (bools[0] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardRarityType.UNCOMMON:
                        if (bools[1] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardRarityType.RARE:
                        if (bools[2] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                }
            }
        }

    }

    public void ReInitCardByType(bool[] bools)
    {
        for (int i = contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentpanel.transform.GetChild(i).gameObject);
        }

        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            if (SaveSystem.Instance.getSave().PlayerGotCards.Contains(item.Key))
            {
                string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
                CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

                switch (cardItem.cardBehaviour.AbilityType)
                {
                    case CardAbilityType.ATTACK:
                        if (bools[0] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardAbilityType.DEFEND:
                        if (bools[1] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardAbilityType.HEAL:
                        if (bools[2] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardAbilityType.EXPAND:
                        if (bools[3] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                    case CardAbilityType.SKILL:
                        if (bools[4] == true)
                        {
                            Initcard(cardItem, item.Value);
                        }
                        break;
                }
            }
        }
    }

    public void Initcard(CardItem cardItem,int value)
    {
        GameObject shopitem = GameObject.Instantiate(ShopItemPrefab, contentpanel.transform) as GameObject;
        shopitem.name = cardItem.Name;
        shopitem.GetComponent<CardItemInventory>().carditem = cardItem;
        shopitem.GetComponent<CardItemInventory>().num = value;
        shopitem.GetComponent<CardItemInventory>().showstate = 0;
        shopitem.GetComponent<CardItemInventory>().ResetNumText();
        GameObject newcard = GameObject.Instantiate(cardItem.cardBehaviour.gameObject, shopitem.transform) as GameObject;
        newcard.transform.SetSiblingIndex(0);
        newcard.gameObject.GetComponent<CardRotate>().enabled = false;
        newcard.gameObject.GetComponent<CardPosition>().enabled = false;
        newcard.gameObject.GetComponent<CardSetTarget>().enabled = false;
        Button button = shopitem.transform.GetChild(1).GetComponent<Button>();
        button.AddComponent<ButtonOfCard>();
    }

    public void Return()
    {
        
        for (int i = PlayerHoldPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(PlayerHoldPanel.transform.GetChild(i).gameObject);
        }

        for(int i = contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentpanel.transform.GetChild(i).gameObject);
        }


        SaveSystem.Instance.ChangePreset(SaveSystem.Instance.getSave().CardPresetIndex);

        int a = 0;
        foreach (var item in SaveSystem.Instance.getSave().PlayerHoldCards)
        {
            a += item.Value;
        }

        if (a <20)
        {
            a = 20 - a;
            SaveSystem.Instance.AddPlayerHoldCards("Militia", a);
           
        }

        SceneManager.LoadScene("Base");
    }
}
