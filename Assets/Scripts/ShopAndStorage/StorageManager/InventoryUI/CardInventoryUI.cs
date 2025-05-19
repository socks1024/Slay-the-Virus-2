using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
//using static UnityEditor.Progress;


public class CardInventoryUI : MonoBehaviour
{
    public RectTransform detailPanel;
    public GameObject ContentPanel;
    public GameObject PlayerHoldPanel;
    public GameObject Detailed;
    public CardItemInventory inventoryitem;

    public Transform content;

    public Vector3 PlayerHoldPanelCardSize = new Vector3(0.2f, 0.25f, 0f);

    public GameObject CardFilterPanel;


    private int sum = 0;
    private int num = 0;
    private List<GameObject> blanks=new List<GameObject>();
    private List<int> chosencards = new List<int>();
    private int[] CardExistInInventory = new int[100];

    private DetailPanel detail;

    private void Awake()
    {
        detailPanel.gameObject.SetActive(false);
        CardFilterPanel.SetActive(false);
        detail = detailPanel.gameObject.GetComponent<DetailPanel>();

        GameObject.Find("PresetButton").GetComponent<ButtonSets>().SetButtonSprite(SaveSystem.Instance.getSave().CardPresetIndex-1);
    }

    public void ShowItem(GameObject card)
    {
        
        Detailed = card;
        inventoryitem = card.GetComponent<CardItemInventory>();
        //Detailed.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;

        if (inventoryitem.showstate==0||inventoryitem.showstate==1)
        {
            //if (inventoryitem.num == 1)
            //{
            //    SetToPlayer();
            //    CardExistInInventory[inventoryitem.index] = 1;
            //}
            //else
            //{
            if (!inventoryitem.carditem.cardBehaviour.UnlimitedInDeck)
            {

                if (SaveSystem.Instance.GetPresetByIndex(SaveSystem.Instance.getSave().CardPresetIndex).ContainsKey(inventoryitem.carditem.Name)&&
                    SaveSystem.Instance.GetPresetByIndex(SaveSystem.Instance.getSave().CardPresetIndex)[inventoryitem.carditem.Name] >= 4)
                {
                    Debug.Log(SaveSystem.Instance.GetPresetByIndex(SaveSystem.Instance.getSave().CardPresetIndex)[inventoryitem.carditem.Name]);
                    return;
                }
                
            }

            int a = 0;
            foreach(var item in SaveSystem.Instance.GetPresetByIndex(SaveSystem.Instance.getSave().CardPresetIndex))
            {
                a += item.Value;
            }

            if (a >= 40)
            {
                return;
            }

           


            if (inventoryitem.num > 0)
            {
                
                SetToPlayerWithoutRemove();
                SaveSystem.Instance.AddPlayerHoldCardsFromInventory(inventoryitem.carditem.Name, 1,SaveSystem.Instance.getSave().CardPresetIndex);
                inventoryitem.ResetNumText();

               
            }
        }
        else if (inventoryitem.showstate == 2)//放回
        {
            inventoryitem.showstate = 0;
            SaveSystem.Instance.AddPlayerHoldCardsFromInventory(inventoryitem.carditem.Name, -1, SaveSystem.Instance.getSave().CardPresetIndex);

            if (content.Find(inventoryitem.carditem.Name))
            {
                content.Find(inventoryitem.carditem.Name).gameObject.GetComponent<CardItemInventory>().ResetNumText();
            }
                GameObject.Destroy(Detailed);
        }
    }

    public void ShowItem(GameObject card,int cardpresetindex)
    {

        Detailed = card;
        inventoryitem = card.GetComponent<CardItemInventory>();
       
        if (inventoryitem.showstate == 0 || inventoryitem.showstate == 1)
        {
            if (inventoryitem.num > 0)
            {
                SetToPlayerWithoutRemove();
                SaveSystem.Instance.AddPlayerHoldCardsFromInventory(inventoryitem.carditem.Name, 1, cardpresetindex);
                inventoryitem.ResetNumText();
            }
        }
        else if (inventoryitem.showstate == 2)
        {
            inventoryitem.showstate = 0;
            SaveSystem.Instance.AddPlayerHoldCardsFromInventory(inventoryitem.carditem.Name, -1, cardpresetindex);
            if (content.Find(inventoryitem.carditem.Name))
            {
                content.Find(inventoryitem.carditem.Name).gameObject.GetComponent<CardItemInventory>().ResetNumText();
            }
            GameObject.Destroy(Detailed);
        }
    }

    public void CancelDetail()//取消预览
    {
        detailPanel.gameObject.SetActive(false);
        Detailed.transform.SetParent( content);
        Detailed.transform.SetSiblingIndex(inventoryitem.index - Search(inventoryitem.index));
        Detailed.transform.localScale = inventoryitem.originalscale;
        Detailed.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
       
        inventoryitem.showstate = 0;
    }
    private int Search(int code)
    {
        int count = 0;
        foreach (int i in chosencards)
        {
            if (i < code)
                count++;
        }
        return count;
    }

    public void SetToPlayerWithoutRemove()
    {
        CancelDetail();
        GameObject newplayercard = Instantiate(Detailed);
        newplayercard.GetComponent<CardItemInventory>().showstate = 2;
        newplayercard.transform.GetChild(2).gameObject.SetActive(false);
        newplayercard.transform.SetParent(PlayerHoldPanel.transform);
        newplayercard.transform.localScale = Detailed.transform.localScale;
        newplayercard.transform.SetSiblingIndex(0);
        newplayercard.GetComponent<CardItemInventory>().index = inventoryitem.index;
       
    }

    public void Preview()
    {
        inventoryitem.showstate = 1;
        detailPanel.gameObject.SetActive(true);
        Detailed.transform.SetParent(detailPanel.transform);
        Detailed.transform.localScale = new Vector3(inventoryitem.originalscale.x * 8f, inventoryitem.originalscale.y * 8f, 0);
        Detailed.transform.localPosition = Vector3.zero;
        Detailed.transform.SetAsFirstSibling();
       

        int price = 0;
        if (inventoryitem.carditem.cardBehaviour.Pack == CardPack.MEDICAL)
        {
            if (inventoryitem.carditem.cardBehaviour.RarityType == CardRarityType.RARE)
            {
                price = 60;
            }
            else if (inventoryitem.carditem.cardBehaviour.RarityType == CardRarityType.UNCOMMON)
            {
                price = 100;
            }
        }
        else
        {
            if (inventoryitem.carditem.cardBehaviour.RarityType == CardRarityType.RARE)
            {
                price = 30;
            }
            else if (inventoryitem.carditem.cardBehaviour.RarityType == CardRarityType.UNCOMMON)
            {
                price = 60;
            }
        }

        detail.price = price;
        detail.CardName = Detailed.name;

        detailPanel.GetComponent<DetailPanel>().Start();
    }



    public void OnOpenCardFilterPanel()
    {
        CardFilterPanel.SetActive(true);
    }
}
