using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    /// <summary>
    /// 游戏盘包含的所有格子
    /// </summary>
    public List<Square> squares;

    /// <summary>
    /// 获取格子
    /// </summary>
    /// <param name="coord">格子相对于原点的坐标</param>
    /// <returns>目标格子,若超出范围则返回null</returns>
    public Square GetSquare(Vector2 coord)
    {
        return null;
    }

    /// <summary>
    /// 获取格子
    /// </summary>
    /// <param name="square">同游戏盘上的另一个格子</param>
    /// <param name="coord">从参数格子到目标格子的向量</param>
    /// <returns>目标格子,若超出范围则返回null</returns>
    public Square GetSquare(Square square,Vector2 coord)
    {
        return GetSquare(GetSquareCoord(square) + coord);
    }

    /// <summary>
    /// 获取格子对应的坐标
    /// </summary>
    /// <param name="square">格子</param>
    /// <returns>格子的坐标</returns>
    public Vector2 GetSquareCoord(Square square)
    {
        return Vector2.zero;
    }

    /// <summary>
    /// 判断卡牌能否被放置在对应的格子
    /// </summary>
    /// <param name="cardData">要放置的卡牌</param>
    /// <param name="square">要被放置的格子</param>
    /// <returns>判断结果</returns>
    public bool CanPlaceCard(CardData cardData,Square square)
    {
        foreach (Vector2 v in cardData.CardShape)
        {
            Square s = GetSquare(square,v);
            if (s == null || !s.Active || s.HasCard)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 将卡牌放置在对应的格子上
    /// </summary>
    /// <param name="cardData">要放置的卡牌</param>
    /// <param name="square">要被放置的格子</param>
    public void PlaceCard(CardData cardData,Square square)
    {
        cardData.transform.position = square.transform.position;
        foreach (Vector2 v in cardData.CardShape)
        {
            Square s = GetSquare(square,v);
            s.CardData = cardData;

            //触发卡牌的ActOnPlaced回调
            cardData.GetComponent<CardBehaviour>().ActOnPlaced();
        }
    }

    /// <summary>
    /// 将某张卡牌从游戏盘上移除
    /// </summary>
    /// <param name="cardData">要移除的卡牌</param>
    public void RemoveCard(CardData cardData)
    {
        foreach (Square square in squares)
        {
            if (square.CardData == cardData)
            {
                square.CardData = null;
            }

            //触发卡牌的ActOnRemoved回调
            cardData.GetComponent<CardBehaviour>().ActOnRemoved();
        }
    }
}
