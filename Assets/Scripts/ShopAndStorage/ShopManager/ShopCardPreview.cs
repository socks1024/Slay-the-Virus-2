using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopCardPreviewPanel : MonoBehaviour, IPointerDownHandler
{

    public ShopUI shopUI;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            shopUI.CancelPreview(transform.GetChild(0).gameObject);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            transform.GetComponentInChildren<CardUI>().Mode = CardMode.BLOCKS;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            transform.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
        }
    }

}
