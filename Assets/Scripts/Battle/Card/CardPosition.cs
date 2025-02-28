using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour
{
    /// <summary>
    /// 卡牌所在格子的绝对坐标
    /// </summary>
    Vector2 cardCoord;

    [HideInInspector]
    /// <summary>
    /// 特殊条件格子
    /// </summary>
    List<Square> ConditionedSquares
    {
        get
        {
            List<Square> result = new List<Square>();

            foreach (Vector2 coord in GetComponent<CardData>().ConditionsShape)
            {
                result.Add(PlayerDeckManager.Instance.board.GetSquare(cardCoord + coord));
            }

            return result;
        }
    }

    /// <summary>
    /// 检查有多少特殊条件格子被占据
    /// </summary>
    /// <returns>被占据的特殊条件格子的数量</returns>
    public int GetSatisfiedSquaresCount()
    {
        int count = 0;

        foreach (Square square in ConditionedSquares)
        {
            if (square.HasCard)
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// 获取满足特殊条件的卡牌
    /// </summary>
    /// <returns>满足特殊条件的卡牌</returns>
    public List<CardData> GetCardsSatisfiedCondition()
    {
        List<CardData> cardDatas= new List<CardData>();

        foreach (Square square in ConditionedSquares)
        {
            if (square.HasCard)
            {
                cardDatas.Add(square.CardData);
            }
        }

        return cardDatas;
    }

    
}
