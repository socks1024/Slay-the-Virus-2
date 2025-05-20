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
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();

        if (cardInteract)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }

        canvasGroup.blocksRaycasts = cardInteract;
        canvasGroup.interactable = cardInteract;
    }

    
}


