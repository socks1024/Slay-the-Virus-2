using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardText : MonoBehaviour
{
    List<string> numbersLabel = new List<string>{
        "damage",
        "defense",
        "heal",
        "effect",
    };

    [Header("关键词")]
    public List<Keyword> keywords;


    string keywordColorCloseTag = "</color>";

    List<Keyword> usedKeywords = new();

    public void RefreshCardText(CardData data, TextMeshProUGUI nameText, TextMeshProUGUI descriptionText)
    {

        string name = data.Name;
        nameText.text = name;

        string description = data.Description;
        descriptionText.text = ReplaceKeywordText(description);

    }

    string ReplaceKeywordText(string text)
    {
        usedKeywords.Clear();

        string newDescription = text;

        foreach (Keyword keyword in keywords)
        {
            if (text.Contains(keyword.name))
            {
                usedKeywords.Add(keyword);
                newDescription = newDescription.Replace(keyword.name, BuildColorTag(keyword.color) + keyword.name + keywordColorCloseTag);
            }
        }

        return newDescription;
    }

    string BuildColorTag(Color color)
    {
        return "<color=#"+ ColorUtility.ToHtmlStringRGB(color) +">";
    }
}

[Serializable]
public struct Keyword
{
    public string name;

    public Color color;
}
