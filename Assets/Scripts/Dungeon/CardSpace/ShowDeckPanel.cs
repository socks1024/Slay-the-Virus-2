using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShowDeckPanel : MonoBehaviour
{
    /// <summary>
    /// 玩家卡组的预制体
    /// </summary>
    public List<CardBehaviour> CardPrefabs{ get{ return DungeonManager.Instance.Player.p_Deck;} }

    /// <summary>
    /// 玩家道具的预制体
    /// </summary>
    public List<RelicBehaviour> RelicPrefabs{ get{ return DungeonManager.Instance.Player.p_Relics;} }

    public ScrollRect CardsView;

    public ScrollRect RelicsView;

    List<CardBehaviour> showedCards = new List<CardBehaviour>();

    List<RelicBehaviour> showedRelics = new List<RelicBehaviour>();

    public void Show()
    {
        SetDeck();
        SetRelics();

        ShowDeck();
    }

    public void Hide()
    {
        ClearDeck();
        ClearRelics();
        gameObject.SetActive(false);
    }

    public void ShowDeck()
    {
        RelicsView.gameObject.SetActive(false);
        CardsView.gameObject.SetActive(true);
    }

    public void ShowRelic()
    {
        RelicsView.gameObject.SetActive(true);
        CardsView.gameObject.SetActive(false);
    }

    void SetDeck()
    {
        foreach (CardBehaviour cardPrefab in CardPrefabs)
        {
            CardBehaviour card = Instantiate(cardPrefab);
            card.transform.SetParent(CardsView.content, false);
            card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
            showedCards.Add(card);
        }
    }

    void ClearDeck()
    {
        for (int i = showedCards.Count - 1; i >= 0; i--)
        {
            Destroy(showedCards[i]);
        }
    }

    void SetRelics()
    {
        foreach (RelicBehaviour relicPrefab in RelicPrefabs)
        {
            RelicBehaviour relic = Instantiate(relicPrefab);
            relic.transform.SetParent(RelicsView.content, false);
            showedRelics.Add(relic);
        }
    }

    void ClearRelics()
    {
        for (int i = showedRelics.Count - 1; i >= 0; i--)
        {
            Destroy(showedRelics[i]);
        }
    }
}
