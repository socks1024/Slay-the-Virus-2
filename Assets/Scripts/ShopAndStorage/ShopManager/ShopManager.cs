using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
// using static UnityEditor.Timeline.Actions.MenuPriority;



//�̵�
public class ShopManager : MonoBehaviour
{

    [SerializeField]
    private List<string> StorageItems = new List<string>();

    public static ShopManager Instance { get; private set; }

    public GameObject shopitemprifab;

    public Transform Contentpanel;

    public ShopUI shopUI;


    private void Start()
    {
        //InitStorage();
        //InitExtract();
        ////for (int i = 0; i < 10; i++) Extract();
        Instance = this;
        
        InitCards();
    }

  

    
    public void Purchase(ShopItemCard shopItemCard)
    {
        SaveSystem.Instance.AddCardToPlayerSave(shopItemCard.item.Name, 1);
        SaveSystem.Instance.AddNutrientToPlayerSave(-shopItemCard.price);
    }

    public void Purchase(ShopItemCard shopItemCard,int num)
    {
        SaveSystem.Instance.AddCardToPlayerSave(shopItemCard.item.Name, num);
        SaveSystem.Instance.AddNutrientToPlayerSave(-shopItemCard.price*num);
    }

    private void InitCards()
    {
        StorageItems.Clear();

        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;

        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
            CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

            switch (cardItem.cardBehaviour.Pack)
            {
                case CardPack.MEDICAL:
                case CardPack.TRAINING:
                    CardInit(cardItem);
                    break;
                case CardPack.WAR_MACHINE:
                    if (levelclear[5] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SHELTER:
                    if (levelclear[4] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.LOGISTICS:
                    if (levelclear[3] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SPECIAL_FORCES:
                    if (levelclear[2] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SIDE_EFFECT:
                    if (levelclear[1] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.AMMO:
                    if (levelclear[0]==true)
                        CardInit(cardItem);
                    break;
            }
        }
    }

    public void ReInitCardsByPack(bool[] bools)
    {
        StorageItems.Clear();
        shopUI.ResetPriceInfo();

        for (int i = Contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Contentpanel.transform.GetChild(i).gameObject);
        }

        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;

        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
            CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

            switch (cardItem.cardBehaviour.Pack)
            {
                case CardPack.MEDICAL:
                    if (bools[0]==true)
                        CardInit(cardItem);
                    break;
                case CardPack.TRAINING:
                    if (bools[1]==true)
                    CardInit(cardItem);
                    break;
                case CardPack.WAR_MACHINE:
                    if (levelclear[5] == true && bools[7] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SHELTER:
                    if (levelclear[4] == true && bools[6] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.LOGISTICS:
                    if (levelclear[3] == true && bools[5] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SPECIAL_FORCES:
                    if (levelclear[2] == true && bools[4] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SIDE_EFFECT:
                    if (levelclear[1] == true && bools[3] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.AMMO:
                    if (levelclear[0] == true && bools[2]==true)
                        CardInit(cardItem);
                    break;
            }
        }

    }

    public void ReInitCardsByRarity(bool[] bools)
    {
        shopUI.ResetPriceInfo();

        for (int i = Contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Contentpanel.transform.GetChild(i).gameObject);
        }

        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;



        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
            CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

           
            if (cardItem.cardBehaviour.RarityType == CardRarityType.UNCOMMON && bools[0] == false)
                continue;
            if (cardItem.cardBehaviour.RarityType == CardRarityType.RARE && bools[1] == false)
                continue;

            switch (cardItem.cardBehaviour.Pack)
            {
                case CardPack.MEDICAL:
                case CardPack.TRAINING:
                    CardInit(cardItem);
                    break;
                case CardPack.WAR_MACHINE:
                    if (levelclear[5] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SHELTER:
                    if (levelclear[4] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.LOGISTICS:
                    if (levelclear[3] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SPECIAL_FORCES:
                    if (levelclear[2] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SIDE_EFFECT:
                    if (levelclear[1] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.AMMO:
                    if (levelclear[0] == true)
                        CardInit(cardItem);
                    break;
            }
        }
    }

    public void ReInitCardsByType(bool[] bools)
    {
        shopUI.ResetPriceInfo();

        for (int i = Contentpanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Contentpanel.transform.GetChild(i).gameObject);
        }

        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;



        foreach (var item in SaveSystem.Instance.getSave().PlayerCardInventory)
        {
            string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + item.Key;
            CardItem cardItem = Resources.Load<CardItem>(FindCardItem);

            if (cardItem.cardBehaviour.AbilityType == CardAbilityType.ATTACK && bools[0]==false)
                continue;
            if (cardItem.cardBehaviour.AbilityType == CardAbilityType.DEFEND && bools[1] == false)
                continue;
            if (cardItem.cardBehaviour.AbilityType == CardAbilityType.HEAL && bools[2] == false)
                continue;
            if (cardItem.cardBehaviour.AbilityType == CardAbilityType.EXPAND && bools[3] == false)
                continue;
            if (cardItem.cardBehaviour.AbilityType == CardAbilityType.SKILL && bools[4] == false)
                continue;


            switch (cardItem.cardBehaviour.Pack)
            {
                case CardPack.MEDICAL:
                case CardPack.TRAINING:
                    CardInit(cardItem);
                    break;
                case CardPack.WAR_MACHINE:
                    if (levelclear[5] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SHELTER:
                    if (levelclear[4] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.LOGISTICS:
                    if (levelclear[3] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SPECIAL_FORCES:
                    if (levelclear[2] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.SIDE_EFFECT:
                    if (levelclear[1] == true)
                        CardInit(cardItem);
                    break;
                case CardPack.AMMO:
                    if (levelclear[0] == true)
                        CardInit(cardItem);
                    break;
            }
        }
    }

    private void CardInit(CardItem item)
    {
        GameObject newcard = GameObject.Instantiate(shopitemprifab, Contentpanel) as GameObject;
        newcard.name = item.Name;
        newcard.GetComponent<ShopItemCard>().item = item;
        //newcard.AddComponent<ButtonConflict>();
        
        newcard.transform.localScale = new Vector3(1.05f, 1.1f, 0);
        

        GameObject card = GameObject.Instantiate(item.cardBehaviour.gameObject, newcard.transform) as GameObject;
        card.transform.SetSiblingIndex(0);

        card.gameObject.GetComponent<CardRotate>().enabled = false;
        card.gameObject.GetComponent<CardPosition>().enabled = false;
        card.gameObject.GetComponent<CardSetTarget>().enabled = false;
        //card.AddComponent<ButtonConflict>();
        card.transform.localScale = new Vector3(75f, 75f, 0);
    }
}




    
  

   



