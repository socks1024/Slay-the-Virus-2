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
        BecomeVisible(cardMode, true);
        BecomeVisible(blockMode, false);
    }

    /// <summary>
    /// 从卡牌模式切换到方块模式
    /// </summary>
    public void SwitchToBlockMode()
    {
        // cardMode.SetActive(false);
        // blockMode.SetActive(true);
        BecomeVisible(cardMode, false);
        BecomeVisible(blockMode, true);
    }

    /// <summary>
    /// 使一个UI物体隐藏或显现
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="visible"></param>
    void BecomeVisible(GameObject gameObject, bool visible)
    {
        foreach (Image img in gameObject.GetComponentsInChildren<Image>())
        {
            if (visible)
            {
                img.color = Color.white;
            }
            else
            {
                img.color = new Color(0,0,0,0);
            }
        }
    }

    
}


