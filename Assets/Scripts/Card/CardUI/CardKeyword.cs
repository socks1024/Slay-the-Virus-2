using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardKeyword : MonoBehaviour
{
    CardBehaviour card;

    [Header("关键词窗口")]
    [SerializeField] Sprite normalBG;
    [SerializeField] Sprite virusBG;
    [SerializeField] GameObject keywordPanel;
    public GameObject tetrisRoot;
    public int tetrisSize;
    public List<SingleKeywordDisplayer> keywordRoots;
    public GameObject showCardRoot;

    void Start()
    {
        card = GetComponent<CardBehaviour>();
        InitKeywords();
    }

    void InitKeywords()
    {
        SetBG();
        HideKeywords();
    }

    void SetBG()
    {
        Sprite bg = normalBG;
        if (card.ActType == CardActType.VIRUS) bg = virusBG;
        
        tetrisRoot.GetComponent<Image>().sprite = bg;
        keywordRoots.ForEach(x => x.GetComponent<Image>().sprite = bg);
    }

    public void SetKeywords(List<Keyword> keywords)
    {
        keywordRoots.ForEach(x => x.gameObject.SetActive(false));

        for (int i = 0; i < keywords.Count; i++)
        {
            keywordRoots[i].gameObject.SetActive(true);
            keywordRoots[i].NameText.text = keywords[i].name;
            keywordRoots[i].DescriptionText.text = keywords[i].description;
        }
    }

    public void SetShowCard(string cardID)
    {
        if (cardID is null) return;

        CardBehaviour card = Instantiate(CardLib.GetCard(cardID), showCardRoot.transform);
        card.transform.localScale = Vector3.one;
        card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
    }

    public void ShowKeywords()
    {
        keywordPanel.SetActive(true);
    }

    public void HideKeywords()
    {
        keywordPanel.SetActive(false);
    }
}
