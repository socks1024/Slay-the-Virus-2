using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{
    /// <summary>
    /// 游戏盘初始的格子启用情况
    /// </summary>
    public bool[,] activateSquares = new bool[5,5];

    /// <summary>
    /// 游戏盘的背景
    /// </summary>
    public Texture2D background;

    /// <summary>
    /// 被填满时结束回合触发的效果
    /// </summary>
    public virtual void ActOnAllFilledTurnEnd(){}
}
