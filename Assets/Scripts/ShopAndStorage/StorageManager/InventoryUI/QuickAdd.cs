using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickAdd : MonoBehaviour,IPointerClickHandler
{
    public CardInventoryUI inventoryUI;
    public GameObject card;
    private void Start()
    {
        inventoryUI = GameObject.Find("CardInventory").GetComponent<CardInventoryUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right&&card.GetComponent<CardItemInventory>().showstate==0)
        {
            inventoryUI.Detailed = card;
            inventoryUI.inventoryitem = card.GetComponent<CardItemInventory>();
            inventoryUI.Preview();
        }
    }
}
