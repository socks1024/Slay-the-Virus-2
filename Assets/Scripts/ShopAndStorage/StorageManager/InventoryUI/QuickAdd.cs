using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickAdd : MonoBehaviour,IPointerClickHandler
{
    private CardInventoryUI inventoryUI;
    public GameObject card;
    private void Awake()
    {
        inventoryUI = GetComponentInParent<CardInventoryUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryUI.Detailed = card;
            inventoryUI.inventoryitem = card.GetComponent<CardItemInventory>();
            inventoryUI.SetToPlayer();
        }
    }
}
