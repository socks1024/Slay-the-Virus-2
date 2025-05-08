using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour
{
    [SerializeField]
    public CardItem item;
    [SerializeField]
    private Toggle toggle;
    private ShopUI shopUI;

    public int index;
    public int price;

    public Transform originalparent;
    public Vector3 originalscale;

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        shopUI = GetComponentInParent<ShopUI>();
        index = transform.GetSiblingIndex();
        originalscale = transform.localScale;
        originalparent = transform.parent;

        if (item.cardBehaviour.Pack==CardPack.MEDICAL)
        {
            if (item.cardBehaviour.RarityType == CardRarityType.RARE)
            {
                price = 60;
            }
            else if(item.cardBehaviour.RarityType == CardRarityType.UNCOMMON)
            {
                price = 100;
            }
        }
        else
        {
            if (item.cardBehaviour.RarityType == CardRarityType.RARE)
            {
                price = 30;
            }
            else if (item.cardBehaviour.RarityType == CardRarityType.UNCOMMON)
            {
                price = 60;
            }
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            AddToBuy();
        }
        else
        {
            RemoveFromBuy();
        }
    }
    public void AddToBuy()
    {
        Debug.Log("Add");
        shopUI.GetOneItem(this);
    }

    public void RemoveFromBuy()
    {
        Debug.Log("Remove");
        shopUI.RemoveOneItem(this);
    }

}
