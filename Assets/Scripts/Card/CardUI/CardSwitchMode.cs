using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSwitchMode : MonoBehaviour
{
    /// <summary>
    /// 卡牌模式的子物体
    /// </summary>
    public GameObject cardMode;

    /// <summary>
    /// 方块模式的子物体
    /// </summary>
    public GameObject blockMode;

    /// <summary>
    /// 从方块模式切换到卡牌模式
    /// </summary>
    public void SwitchToCardMode()
    {
        // cardMode.SetActive(true);
        // blockMode.SetActive(false);
        SetCardInteract(cardMode, true);
        SetCardInteract(blockMode, false);
    }

    /// <summary>
    /// 从卡牌模式切换到方块模式
    /// </summary>
    public void SwitchToBlockMode()
    {
        // cardMode.SetActive(false);
        // blockMode.SetActive(true);
        SetCardInteract(cardMode, false);
        SetCardInteract(blockMode, true);
    }

    void SetCardInteract(GameObject gameObject, bool cardInteract)
    {
        VisualUI.SetVisible(gameObject, cardInteract);

        if (cardInteract) gameObject.transform.SetAsLastSibling();

        // foreach (CardDrag c in gameObject.GetComponentsInChildren<CardDrag>())
        // {
        //     c.enabled = cardInteract;
        // }

        // foreach (CardHover c in gameObject.GetComponentsInChildren<CardHover>())
        // {
        //     c.enabled = cardInteract;
        // }

        // foreach (CardPress c in gameObject.GetComponentsInChildren<CardPress>())
        // {
        //     c.enabled = cardInteract;
        // }
    }

    
}


