using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryUI : MonoBehaviour
{
    public RectTransform detailPanel;
    public GameObject ContentPanel;
    private GameObject Detailed;
    private CardItemInventory inventoryitem;
    public int sum = 0;
    private int num = 0;
    private List<GameObject> blanks=new List<GameObject>();
    private List<int> chosencards = new List<int>();
    
    
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
            inventoryitem.showstate = 1;
            detailPanel.gameObject.SetActive(true);
            Detailed.transform.SetParent (detailPanel.transform);
            Detailed.transform.localScale = inventoryitem.originalscale * 2.0f;
            Detailed.transform.localPosition = Vector3.zero;
        }
      else if (inventoryitem.showstate == 1)
        {
            CancelDetail();
            inventoryitem.showstate = 2;
            ClearBlank();
            sum += 1;
            num = 4 - (sum % 4);
            if (num == 4)
                num = 0;
            Detailed.transform.SetSiblingIndex(0);
            PlayerHold.Instance.AddCard(inventoryitem.carditem.cardData);
            chosencards.Add(inventoryitem.index);
            FillBlank();
        }
        else if (inventoryitem.showstate == 2)
        {
            inventoryitem.showstate = 0;
            ClearBlank();
            Debug.Log(inventoryitem.originalparent.childCount - 8);
            Detailed.transform.SetParent (null);
            Detailed.transform.SetParent(inventoryitem.originalparent);
            Detailed.transform.SetSiblingIndex(inventoryitem.index+sum+num-Search(inventoryitem.index)-1);
            PlayerHold.Instance.RemoveCard(inventoryitem.carditem.cardData);
            chosencards.Remove(inventoryitem.index);
            sum -= 1;
            num = 4 - (sum % 4);
            if (num == 4)
                num = 0;
            FillBlank();
        }
    }

    public void CancelDetail()
    {
        detailPanel.gameObject.SetActive(false);
        Detailed.transform.SetParent( inventoryitem.originalparent);
        Detailed.transform.SetSiblingIndex(inventoryitem.index + sum + num - Search(inventoryitem.index));
        Debug.Log(Search(inventoryitem.index));
        Detailed.transform.localScale = inventoryitem.originalscale;
        Detailed.transform.localPosition = inventoryitem.originalposition;
        inventoryitem.showstate = 0;
    }

    private void FillBlank()
    {
        for(int i = 0; i < num; i++)
        {
            GameObject go = new GameObject();
            blanks.Add(go);
            go.AddComponent<RectTransform>();
            go.transform.SetParent(ContentPanel.transform);
            go.transform.SetSiblingIndex(sum);
        }
    }

    private void ClearBlank()
    {
        foreach (GameObject blank in blanks)
        {
            Destroy(blank.gameObject);
        }
        blanks.Clear();
    }

    private int Search(int code)
    {
        int count=0;
        foreach(int i in chosencards)
        {
            if (i < code)
                count++;
        }
        return count;
    }
}
