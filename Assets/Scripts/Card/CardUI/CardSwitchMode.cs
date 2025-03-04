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
        VisualUI.SetVisible(cardMode, true);
        VisualUI.SetVisible(blockMode, false);
    }

    /// <summary>
    /// 从卡牌模式切换到方块模式
    /// </summary>
    public void SwitchToBlockMode()
    {
        // cardMode.SetActive(false);
        // blockMode.SetActive(true);
        VisualUI.SetVisible(cardMode, false);
        VisualUI.SetVisible(blockMode, true);
    }

    

    
}


