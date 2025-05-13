using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetailPanel : MonoBehaviour,IPointerDownHandler
{
    CardInventoryUI cardInventoryUI;

    public TMPro.TMP_Text BuyText;
    public TMPro.TMP_Text SellText;

    public int price;
    public string CardName;
    
    private void Awake() { 
        cardInventoryUI = GameObject.Find("CardInventory").GetComponent<CardInventoryUI>();

    }

    public void Start()
    {
        transform.GetChild(0).gameObject.GetComponentInChildren<CardKeyword>().ShowKeywords();
        BuyText.text = "¹ºÂò£º" + price.ToString();
        SellText.text = "³öÊÛ£º" + (price * 0.5).ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.transform.GetChild(0).gameObject.GetComponentInChildren<CardKeyword>().HideKeywords();
            cardInventoryUI.CancelDetail();
            //transform.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
            //Debug.Log("cancel");
        }
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse1))
    //    {
    //        transform.GetComponentInChildren<CardUI>().Mode=CardMode.BLOCKS;
    //    }

    //    if (Input.GetKeyUp(KeyCode.Mouse1))
    //    {
    //        transform.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
    //    }
    //}

    public void BuyCard()
    {
        if (SaveSystem.Instance.getSave().Nutrient >= price)
        {
            SaveSystem.Instance.AddNutrientToPlayerSave(-price);
            SaveSystem.Instance.AddCardToPlayerSave(CardName, 1);
            transform.GetChild(0).gameObject.GetComponent<CardItemInventory>().ResetNumText();
        }
    }

    public void SellCard()
    {
        if (SaveSystem.Instance.getSave().PlayerCardInventory[CardName] > 0)
        {
            SaveSystem.Instance.AddNutrientToPlayerSave(price/2);
            SaveSystem.Instance.AddCardToPlayerSave(CardName, -1);
            transform.GetChild(0).gameObject.GetComponent<CardItemInventory>().ResetNumText();
        }
    }
}
