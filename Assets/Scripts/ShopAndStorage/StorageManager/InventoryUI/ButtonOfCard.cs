using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOfCard : MonoBehaviour
{
    private Button button;
    private CardInventoryUI cardInventoryUI;
    private GameObject cardItem;

    private void Awake()
    {
        button = GetComponent<Button>();
        cardInventoryUI = GameObject.Find("CardInventory").GetComponent<CardInventoryUI>();
        cardItem = this.transform.parent.gameObject;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        cardInventoryUI.ShowItem(cardItem);
    }

}
