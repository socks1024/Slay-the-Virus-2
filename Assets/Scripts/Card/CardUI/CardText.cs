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
        "damage",
        "defense",
        "heal",
        "effect",
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
                    case "damage":
                        description.Replace(label, "");
                        break;
                    case "defense":
                        description.Replace(label, "");
                        break;
                    
                }
            }
        }

        return description;
    }

    #endregion

    public void RefreshCardText(CardData data, TextMeshProUGUI nameText, TextMeshProUGUI descriptionText)
    {

        string name = data.Name;
        nameText.text = name;

        string description = data.Description;
        description = ReplaceKeywordText(description);

        descriptionText.text = description;
    }
}

[Serializable]
public struct Keyword
{
    public string name;

    public Color color;
}
