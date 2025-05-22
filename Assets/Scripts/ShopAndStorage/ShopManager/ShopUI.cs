using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public TextMeshProUGUI resource;
    public TextMeshProUGUI price;
    public GameObject content;
    public GameObject previewPanel;
    public TMPro.TMP_Text storagenum;

    public float previewExpand_x = 1.60f;
    public float previewExpand_y = 1.75f;

    private List<ShopItemCard> items = new List<ShopItemCard>();
    private int sumprice;


    public int num = 1;

    public TMPro.TMP_Text numtext;


    public GameObject CardFilterPanelByPack;
    public GameObject CardFilterPanelByRarity;
    public GameObject CardFilterPanelByType;

    public void Start()
    {
       resource.text = SaveSystem.Instance.getSave().Nutrient.ToString();
        if (SaveSystem.Instance.getSave().TutorialClear[4] == false)
        {
            DialogueManager.Instance.StartDialogue("shop");
            SaveSystem.Instance.SetTutorialClear(4);
        }

        ResetNumtext();

        CardFilterPanelByPack.SetActive(false);
        CardFilterPanelByRarity.SetActive(false);
        CardFilterPanelByType.SetActive(false);
    }

    private void ResetNumtext()
    {
        numtext.text = num.ToString();
    }

    public void AddNum()
    {
        if (num < 10)
        {
            num++;
            ResetNumtext();

            UpdatePrice();
            price.text = sumprice.ToString();
        }
    }

    public void Subnum()
    {
        if (num > 1)
        {
            num--;
            ResetNumtext();

            UpdatePrice();
            price.text = sumprice.ToString();
        }
    }


    public void GetOneItem(ShopItemCard oneitem)
    {
        Debug.Log("price change!");
        items.Add(oneitem);
        UpdatePrice();
        price.text = sumprice.ToString();
        resourcetextcolor();
    }

    private void UpdatePrice()
    {
        sumprice = 0;
       foreach(ShopItemCard oneitem in items)
        {
            sumprice += oneitem.price;
        }

        sumprice *= num;
    }

    public void RemoveOneItem(ShopItemCard item)
    {
        items.Remove(item);
        UpdatePrice();
        price.text = sumprice.ToString();
        resourcetextcolor();
        if (items.Count == 0)
            resource.color = Color.black;
    }

    public void trybuysth()
    {
        if (SaveSystem.Instance.getSave().Nutrient<sumprice)
        {
            return;
        }

        foreach (ShopItemCard item in items)
        {
            ShopManager.Instance.Purchase(item,num);
        }
        resource.text = SaveSystem.Instance.getSave().Nutrient.ToString();
        resourcetextcolor();
    }

    private void resourcetextcolor()
    {
        if (SaveSystem.Instance.getSave().Nutrient >= sumprice)
            resource.color = Color.green;
        else resource.color = Color.red;
    }


    public void OnReturn()
    {
        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        SceneManager.LoadScene("Base");
    }

    public void PreviewCard(GameObject card)
    {
        previewPanel.SetActive(true);
        string name = card.GetComponent<ShopItemCard>().item.Name;
        int num = SaveSystem.Instance.getSave().PlayerCardInventory[name];
        if (SaveSystem.Instance.getSave().PlayerHoldCards.ContainsKey(name))
        {
            num+= SaveSystem.Instance.getSave().PlayerHoldCards[name];
        }

        storagenum.text = "¿â´æÊý:" + num.ToString();

        card.transform.SetParent(previewPanel.transform);
        card.transform.SetAsFirstSibling();
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(card.GetComponent<ShopItemCard>().originalscale.x * previewExpand_x, card.GetComponent<ShopItemCard>().originalscale.y * previewExpand_y, 0);
        previewPanel.gameObject.GetComponent<ShopCardPreviewPanel>().Start();

    }

    public void CancelPreview(GameObject card)
    {
        previewPanel.SetActive(false);

        card.transform.SetParent(content.transform);
        card.transform.SetSiblingIndex(card.GetComponent<ShopItemCard>().index);
        card.transform.localScale = card.GetComponent<ShopItemCard>().originalscale;

        card.transform.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
    }

    public void OnOpenCardFilterPanelByPack()
    {
        CardFilterPanelByPack.SetActive(true);
    }

    public void OnOpenCardFilterPaenlByRarity()
    {
        CardFilterPanelByRarity.SetActive(true);
    }

    public void OnOpenCardFilterPaenlByType()
    {
        CardFilterPanelByType.SetActive(true);
    }

    public void ResetPriceInfo()
    {
        items.Clear();
        UpdatePrice();
        price.text = sumprice.ToString();
        resourcetextcolor();
    }
}
