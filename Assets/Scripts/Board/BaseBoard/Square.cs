using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    /// <summary>
    /// 该格子相对于原点的坐标
    /// </summary>
    public Vector2 squareCoord;

    /// <summary>
    /// 该格子能否被放置卡牌
    /// </summary>
    public bool IsActive
    { 
        get { return isActive; }
        set 
        { 
            isActive = value; 
            if (IsActive)
            {
                GetComponent<Image>().color = new Color(0,0,0,0);
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
        }
    }
    bool isActive = true;

    [HideInInspector]
    /// <summary>
    /// 已经填充了卡牌
    /// </summary>
    public bool HasCard{ get { return CardData != null; } }

    [HideInInspector]
    /// <summary>
    /// 被填充的卡牌
    /// </summary>
    public CardBehaviour CardData{ get; set; }

    /// <summary>
    /// 对当前格子上的卡牌所作的调整
    /// </summary>
    public UnityAction<CardBehaviour> CardAdjustment;

    /// <summary>
    /// 对当前格子上的卡牌应用调整
    /// </summary>
    public void AdjustCardOnSquare()
    {
        if (HasCard)
        {
            CardAdjustment?.Invoke(CardData);
        }
    }
}
