using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardItemInventory : MonoBehaviour
{
    public CardItem carditem;
    public int index;
    public Vector3 originalscale;
    public Vector3 originalposition;
    public Transform originalparent;
    public int showstate = 0;
    public int num=0;
    public TMPro.TMP_Text numtext;
   
   
    void Awake()
    {
        index = transform.GetSiblingIndex();
        originalposition = transform.position;
        originalscale = transform.localScale;
        originalparent = transform.parent;
        numtext.text = num.ToString();

    }

    public void ResetNumText()
    {
        num = SaveSystem.Instance.getSave().PlayerCardInventory[carditem.Name];
        numtext.text = num.ToString();
    }

}
