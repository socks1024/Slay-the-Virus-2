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

    string keywordColorCloseTag = "</color>";

    List<Keyword> usedKeywords = new();

    string ReplaceKeywordText(string text)
    {
        usedKeywords.Clear();

        foreach (Keyword keyword in keywords)
        {
            if (text.Contains(keyword.name))
            {
                usedKeywords.Add(keyword);
                text = text.Replace(keyword.name, BuildColorTag(keyword.color) + keyword.name + keywordColorCloseTag);
            }
        }

        return text;
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

    CardUI cardUI;

    CardData data;

    public void RefreshCardText()
    {
        string name = data.Name;
        cardUI.nameText.text = name;

        string description = data.Description;

        description = ReplaceKeywordText(description);
        description = ReplaceNumbersText(description);

        cardUI.descriptionText.text = description;
    }

    void Start()
    {
        cardUI = GetComponent<CardUI>();
        data = GetComponent<CardBehaviour>().cardData;
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

    public Color color;
}
