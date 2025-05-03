using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        button.AddComponent<ButtonConflict>();
        cardInventoryUI = GameObject.Find("CardInventory").GetComponent<CardInventoryUI>();
        cardItem = this.transform.parent.gameObject;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        cardInventoryUI.ShowItem(cardItem);
    }

}
