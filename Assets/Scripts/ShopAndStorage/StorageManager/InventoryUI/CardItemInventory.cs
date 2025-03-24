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
   
   
    void Start()
    {
        index = transform.GetSiblingIndex();
        originalposition = transform.position;
        originalscale = transform.localScale;
        originalparent = transform.parent;
        
    }
   
}
