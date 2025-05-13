using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Progress;


public class CardInventoryUI : MonoBehaviour
{
    public RectTransform detailPanel;
    public GameObject ContentPanel;
    public GameObject PlayerHoldPanel;
    //public EnemyBehaviour enemyBehaviour;
    //public BoardBehaviour boardBehaviour;
    public GameObject Detailed;
    public CardItemInventory inventoryitem;

    public Transform content;

    public Vector3 PlayerHoldPanelCardSize = new Vector3(0.2f, 0.25f, 0f);


    private int sum = 0;
    private int num = 0;
    private List<GameObject> blanks=new List<GameObject>();
    private List<int> chosencards = new List<int>();
    private int[] CardExistInInventory = new int[100];

    private DetailPanel detail;

    private void Awake()
    {
        detailPanel.gameObject.SetActive(false);
        detail = detailPanel.gameObject.GetComponent<DetailPanel>();
        
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
            if (inventoryitem.num > 0)
            {
                SetToPlayerWithoutRemove();
                SaveSystem.Instance.AddPlayerHoldCardsFromInventory(inventoryitem.carditem.Name, 1);
                inventoryitem.ResetNumText();

                //Debug.Log("Add!");
            }
                //inventoryitem.num--;
            //}
            
            //Preview();
        }
        else if (inventoryitem.showstate == 2)//放回
        {
            inventoryitem.showstate = 0;
            //ClearBlank();
            SaveSystem.Instance.AddPlayerHoldCardsFromInventory(inventoryitem.carditem.Name, -1);
            //if (CardExistInInventory[inventoryitem.index]==1)
            //{
            //    Detailed.transform.SetParent(null);
            //    Detailed.transform.SetParent(content);
            //    Detailed.transform.GetChild(2).gameObject.SetActive(true);
            //    Detailed.transform.SetSiblingIndex(inventoryitem.index - Search(inventoryitem.index));
            //    Detailed.transform.localScale = inventoryitem.originalscale;
            //    //inventoryitem.num = 1;
            //    inventoryitem.ResetNumText();
            //    CardExistInInventory[inventoryitem.index] = 0;
            //    //PlayerHold.Instance.RemoveCard(inventoryitem.carditem.cardBehaviour);
            //    chosencards.Remove(inventoryitem.index);
            //}
            //else
            //{
                //Debug.Log("inventoryitem.originalparent");
                //content.GetChild(inventoryitem.index - Search(inventoryitem.index)).gameObject.GetComponent<CardItemInventory>().num++;
                content.GetChild(inventoryitem.index - Search(inventoryitem.index)).gameObject.GetComponent<CardItemInventory>().ResetNumText();
                //PlayerHold.Instance.RemoveCard(inventoryitem.carditem.cardBehaviour);
                GameObject.Destroy(Detailed);
            //}

            

            //sum -= 1;
            //num = 4 - (sum % 4);
            //if (num == 4)
            //    num = 0;
            //if(sum>0)
            //FillBlank();
        }
    }

    public void CancelDetail()//取消预览
    {
        detailPanel.gameObject.SetActive(false);
        Detailed.transform.SetParent( content);
        Detailed.transform.SetSiblingIndex(inventoryitem.index - Search(inventoryitem.index));
        Detailed.transform.localScale = inventoryitem.originalscale;
        Detailed.GetComponentInChildren<CardUI>().Mode = CardMode.CARD;
        //Detailed.transform.localPosition = inventoryitem.originalposition;
        inventoryitem.showstate = 0;
    }

    //private void FillBlank()
    //{
    //    for(int i = 0; i < num; i++)
    //    {
    //        GameObject go = new GameObject();
    //        blanks.Add(go);
    //        go.AddComponent<RectTransform>();
    //        go.transform.SetParent(ContentPanel.transform);
    //        go.transform.SetSiblingIndex(sum);
    //    }
    //}

    //private void ClearBlank()
    //{
    //    foreach (GameObject blank in blanks)
    //    {
    //        Destroy(blank.gameObject);
    //    }
    //    blanks.Clear();
    //}

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

    //public void TranslateToBattleTest()//仅供测试用
    //{
    //    List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();
    //    for(int i = 0; i < 2; i++)
    //    {
    //        enemies.Add(enemyBehaviour);
    //    }
    //    Messenger.enterBattleInfoTest.p_Enemies = enemies;
    //    Messenger.enterBattleInfoTest.p_Board = boardBehaviour;
    //    Messenger.enterBattleInfoTest.p_Cards = PlayerHold.Instance.GetCardBehaviours();
    //    SceneManager.LoadScene("BattleScene");

    //    //DungeonManager.Instance.EnterBattleForTest(PlayerHold.Instance.GetCardBehaviours(), boardBehaviour, enemies);
    //}

    //public void SetToPlayer()
    //{
    //    CancelDetail();
    //    inventoryitem.showstate = 2;
    //    //ClearBlank();
    //    sum += 1;
    //    num = 4 - (sum % 4);
    //    Detailed.transform.GetChild(2).gameObject.SetActive(false);
    //    Detailed.transform.SetParent(PlayerHoldPanel.transform);
    //    Detailed.transform.localScale = PlayerHoldPanelCardSize;
    //    Detailed.transform.SetSiblingIndex(0);
    //    //PlayerHold.Instance.AddCard(inventoryitem.carditem.cardBehaviour);
    //    chosencards.Add(inventoryitem.index);
    //    //if(sum>0)
    //    //FillBlank();
    //}

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
        //PlayerHold.Instance.AddCard(inventoryitem.carditem.cardBehaviour);
        //chosencards.Add(inventoryitem.index);

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
}
