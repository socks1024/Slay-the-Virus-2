using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour
{
    [SerializeField]
    private CardItem carditem;
    [SerializeField]
    private Toggle toggle;
    private ShopUI shopUI;

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        toggle.group = GetComponentInParent<ToggleGroup>();
        shopUI = GetComponentInParent<ShopUI>();
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
            shopUI.RemoveOneItem();
        }
    }
    public void AddToBuy()
    {
        Debug.Log("Add");
        shopUI.GetOneItem(carditem);
    }

    public void RemoveFromBuy()
    {
        Debug.Log("Remove");
        shopUI.RemoveOneItem();
    }

}
