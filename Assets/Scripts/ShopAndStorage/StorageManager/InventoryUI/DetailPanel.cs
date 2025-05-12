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

    public void Start()
    {
        transform.GetChild(0).gameObject.GetComponentInChildren<CardKeyword>().ShowKeywords();
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
}
