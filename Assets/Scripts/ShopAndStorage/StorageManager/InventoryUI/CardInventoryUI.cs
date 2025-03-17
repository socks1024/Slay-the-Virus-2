using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryUI : MonoBehaviour
{
    public RectTransform detailPanel;
    private GameObject Detailed;
    private CardItemInventory inventoryitem;
    
    private void Awake()
    {
        detailPanel.gameObject.SetActive(false);
    }

    public void ShowItem(GameObject card)
    {
        
        Detailed = card;
        inventoryitem = card.GetComponent<CardItemInventory>();
        

        if (inventoryitem.showstate==0)
        {
            detailPanel.gameObject.SetActive(true);
            Detailed.transform.parent = detailPanel.transform;
            Detailed.transform.localScale = inventoryitem.originalscale * 2.0f;
            Detailed.transform.localPosition = Vector3.zero;
            inventoryitem.showstate = 1;
        }
      else if (inventoryitem.showstate == 1)
        {
            CancelDetail();
            Detailed.transform.SetSiblingIndex(0);
            PlayerHold.Instance.AddCard(inventoryitem.carditem.cardData);
            inventoryitem.showstate = 2;
        }
        else if (inventoryitem.showstate == 2)
        {
            Detailed.transform.SetSiblingIndex(inventoryitem.index);
            PlayerHold.Instance.RemoveCard(inventoryitem.carditem.cardData);
            inventoryitem.showstate = 0;
        }
    }

    public void CancelDetail()
    {
        detailPanel.gameObject.SetActive(false);
        Detailed.transform.SetParent( inventoryitem.originalparent);
        Detailed.transform.SetSiblingIndex(inventoryitem.index);
        Detailed.transform.localScale = inventoryitem.originalscale;
        Detailed.transform.localPosition = inventoryitem.originalposition;
       
    }
}
