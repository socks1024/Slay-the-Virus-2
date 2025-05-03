using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EnterBattlePanel : MonoBehaviour
{
    public DungeonMissionData MissionData;

    public Image EnemyImage;
    public TMPro.TMP_Text Nametext;
    public TMPro.TMP_Text Descriptiontext;
    public TMPro.TMP_Text Tiptext;
    public GameObject[] DangerLevel;
    

    public void Start()
    {
        

        EnemyImage.sprite = MissionData.EnemyImage;

        Nametext.text = MissionData.DungeonName;
        Descriptiontext.text = MissionData.EnemyDescription;
        Tiptext.text = MissionData.Tips;

        for(int i = 0; i < 3; i++)
        {
            DangerLevel[i].SetActive(false);
        }

        for(int i = 0; i < MissionData.RiskLevel; i++)
        {
            DangerLevel[i].SetActive(true);
        }
    }

    public void EnterTestBattle()
    {
        //Dictionary<CardItem, int> playercardsindictionary = PlayerHold.Instance.GetPlayerHoldCard();

        List<CardBehaviour> playecardBehaviours=new List<CardBehaviour>();
        foreach(var card in SaveSystem.Instance.getSave().PlayerHoldCards)
        {
            for(int i = 0; i < card.Value; i++)
            {
                string FindCardItem = "ScriptableObjects/StorageAndShop/Cards/" + card.Key;
                CardItem cardItem = Resources.Load<CardItem>(FindCardItem);
                playecardBehaviours.Add(cardItem.cardBehaviour);
            }
        }

        Messenger.enterDungeonInfo.p_Cards = playecardBehaviours;
        Messenger.enterDungeonInfo.p_Relics = new List<RelicBehaviour>();
        Messenger.enterDungeonInfo.missionData = MissionData;

        SceneManager.LoadScene("BattleScene");
    }
}
