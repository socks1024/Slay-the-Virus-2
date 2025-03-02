using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        set { isActive = value; }
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
}
