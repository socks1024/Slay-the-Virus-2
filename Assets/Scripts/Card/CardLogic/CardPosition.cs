using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardPosition : MonoBehaviour
{
    /// <summary>
    /// 卡牌所在格子的绝对坐标
    /// </summary>
    Vector2 cardCoord;

    /// <summary>
    /// 当前的棋盘
    /// </summary>
    BoardBehaviour board{ get{ return DungeonManager.Instance.battleManager.board; } }

    /// <summary>
    /// 特殊条件格子
    /// </summary>
    [HideInInspector]public List<Square> ConditionedSquares
    {
        get
        {
            List<Square> result = new List<Square>();

            foreach (Vector2 coord in GetComponent<CardBehaviour>().ConditionsShape)
            {
                result.Add(board.GetSquare(cardCoord + coord));
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

    public bool Conditioned{ get{ return GetSatisfiedSquaresCount() > 0;}}

    /// <summary>
    /// 获取满足特殊条件的卡牌
    /// </summary>
    /// <returns>满足特殊条件的卡牌</returns>
    public List<CardBehaviour> GetCardsSatisfiedCondition()
    {
        List<CardBehaviour> cardDatas= new List<CardBehaviour>();

        foreach (Square square in ConditionedSquares)
        {
            if (square.HasCard)
            {
                cardDatas.Add(square.CardData);
            }
        }

        return cardDatas;
    }

    /// <summary>
    /// 将卡牌调整设置到条件格子上
    /// </summary>
    public void AddCardAdjustmentToConditionedSquares(UnityAction<CardBehaviour> adjustment)
    {
        ConditionedSquares.ForEach(square => { square.CardAdjustment += adjustment; });
    }

    /// <summary>
    /// 将卡牌调整从条件格子上移除
    /// </summary>
    public void RemoveCardAdjustmentFromConditionedSquares(UnityAction<CardBehaviour> adjustment)
    {
        ConditionedSquares.ForEach(square => { square.CardAdjustment -= adjustment; });
    }
}
