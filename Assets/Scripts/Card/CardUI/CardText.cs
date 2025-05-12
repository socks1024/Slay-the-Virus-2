using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardText : MonoBehaviour
{

    #region Keywords

    [Header("关键词")]
    public List<Keyword> keywords;

    string colorCloseTag = "</color>";

    List<Keyword> usedKeywords = new();

    string ReplaceKeywordText(string text)
    {
        usedKeywords.Clear();

        foreach (Keyword keyword in keywords)
        {
            if (text.Contains(keyword.name))
            {
                usedKeywords.Add(keyword);
                text = text.Replace(keyword.name, BuildColorTag(keyword.color) + keyword.name + colorCloseTag);
            }
        }

        return text;
    }

    void IdentifyKeywords(string text)
    {
        usedKeywords.Clear();

        foreach (Keyword keyword in keywords)
        {
            if (text.Contains(keyword.name))
            {
                usedKeywords.Add(keyword);
            }
        }
    }

    string BuildColorTag(Color color)
    {
        return "<color=#"+ ColorUtility.ToHtmlStringRGB(color) +">";
    }

    #endregion

    #region Numbers

    List<string> numbersLabel = new List<string>{
        "#d",
        "#b",
        "#h",
        "#e",
    };

    CardBehaviour cardBehaviour{ get{ return GetComponent<CardBehaviour>(); } }

    string ReplaceNumbersText(string description)
    {
        foreach (string label in numbersLabel)
        {
            if (description.Contains(label))
            {
                switch (label)
                {
                    case "#d":
                        description = description.Replace(label, cardBehaviour.nextDamage.ToString());
                        break;
                    case "#b":
                        description = description.Replace(label, cardBehaviour.nextDefense.ToString());
                        break;
                    case "#h":
                        description = description.Replace(label, cardBehaviour.nextHeal.ToString());
                        break;
                    case "#e":
                        description = description.Replace(label, cardBehaviour.nextEffect.ToString());
                        break;
                }
            }
        }

        return description;
    }

    #endregion

    #region Sprites

    [Header("精灵字符")]
    public List<SpriteLabel> spriteLabels;

    string ReplaceSpritesText(string description)
    {
        foreach (SpriteLabel label in spriteLabels)
        {
            if (description.Contains(label.name))
            {
                description = description.Replace(label.name, "<sprite=" + label.index.ToString() + ">");
            }
        }

        return description;
    }

    #endregion

    #region CardName

    [Header("卡牌标记")]
    public Color CellCardColor;
    public Color VirusCardColor;

    string usedCardID;

    string ReplaceCardNameText(string description)
    {
        foreach (string cardID in CardLib.cardPrefabs.Keys)
        {
            if (description.Contains(cardID))
            {
                string cardName = CardLib.GetCard(cardID).cardData.Name;

                int index = 0;
                Color color = CellCardColor;
                switch(CardLib.GetCard(cardID).ActType)
                {
                    case CardActType.BATTLE_FIELD:
                        index = 2;
                        break;
                    case CardActType.COMMAND:
                        index = 1;
                        break;
                    case CardActType.VIRUS:
                        index = 0;
                        color = VirusCardColor;
                        break;                    
                }

                description = description.Replace(cardID, "<sprite=" + index.ToString() + ">" + BuildColorTag(color) + cardName + colorCloseTag);
            }
            
        }

        return description;
    }

    void IdentifyCardIDs(string text)
    {
        usedCardID = null;

        foreach (string cardID in CardLib.cardPrefabs.Keys)
        {
            if (text.Contains(cardID))
            {
                usedCardID = cardID;
            }
        }
    }

    #endregion

    CardUI cardUI;

    CardData data;

    CardKeyword cardKeyword;

    public void RefreshCardText()
    {
        string name = data.Name;
        cardUI.nameText.text = name;

        string description = data.Description;

        description = ReplaceKeywordText(description);
        description = ReplaceNumbersText(description);
        description = ReplaceSpritesText(description);
        description = ReplaceCardNameText(description);

        cardUI.descriptionText.text = description;
    }

    void Start()
    {
        cardUI = GetComponent<CardUI>();
        data = GetComponent<CardBehaviour>().cardData;
        cardKeyword = GetComponent<CardKeyword>();

        IdentifyKeywords(data.Description);
        IdentifyCardIDs(data.Description);

        cardKeyword.SetKeywords(usedKeywords);
        cardKeyword.SetShowCard(usedCardID);
    }

    void Update()
    {
        RefreshCardText();
    }
}

[Serializable]
public struct Keyword
{
    public string name;

    [TextArea]
    public string description;

    public Color color;
}

[Serializable]
public struct SpriteLabel
{
    public string name;

    public int index;
}
