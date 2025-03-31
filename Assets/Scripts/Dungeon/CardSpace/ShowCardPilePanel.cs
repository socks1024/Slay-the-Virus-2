using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCardPilePanel : MonoBehaviour
{
    /// <summary>
    /// 要显示的牌堆
    /// </summary>
    public CardPile pile;

    /// <summary>
    /// 窗口的根物体
    /// </summary>
    public Transform content;

    /// <summary>
    /// 展示卡牌
    /// </summary>
    public void ShowCards()
    {
        foreach (CardBehaviour card in pile.GetCards())
        {
            card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
            card.gameObject.transform.SetParent(content, false);
        }
    }

    /// <summary>
    /// 结束展示卡牌
    /// </summary>
    public void ClearCards()
    {
        for (int i = 0; i < pile.Count; i++)
        {
            pile.GetCards()[i].transform.SetParent(DungeonManager.Instance.storage, false);
        }

        gameObject.SetActive(false);
    }
}


