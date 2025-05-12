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
            transform.GetChild(0).gameObject.GetComponentInChildren<CardKeyword>().HideKeywords();
            shopUI.CancelPreview(transform.GetChild(0).gameObject);
        }
    }

    public void Start()
    {
        transform.GetChild(0).gameObject.GetComponentInChildren<CardKeyword>().ShowKeywords();
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse1))
    //    {
    //        transform.GetComponentInChildren<CardUI>().Mode = CardMode.BLOCKS;
    //    }

    //    if (Input.GetKeyUp(KeyCode.Mouse1))
    //    {
    //        transform.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
    //    }
    //}

}
