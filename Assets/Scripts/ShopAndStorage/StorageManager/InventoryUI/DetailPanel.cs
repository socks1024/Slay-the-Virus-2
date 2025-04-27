using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetailPanel : MonoBehaviour,IPointerDownHandler
{
    CardInventoryUI cardInventoryUI;

    
    private void Awake() { 
        cardInventoryUI = GameObject.Find("CardInventory").GetComponent<CardInventoryUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            cardInventoryUI.CancelDetail();
            Debug.Log("cancel");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            transform.GetComponentInChildren<CardUI>().Mode=CardMode.BLOCKS;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            transform.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
        }
    }
}
