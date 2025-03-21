using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetailPanel : MonoBehaviour,IPointerDownHandler
{
    CardInventoryUI cardInventoryUI;

    private void Awake()
    {
        cardInventoryUI = GetComponentInParent<CardInventoryUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("cancel");
        cardInventoryUI.CancelDetail();
    }
}
