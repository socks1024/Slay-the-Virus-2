using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardPosition : MonoBehaviour
{
    /// <summary>
    /// 卡牌所在格子的绝对坐标
    /// </summary>
    public Vector2 cardCoord = -Vector3.one;

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

            if (cardCoord == -Vector2.one)
            {
                return null;
            }

            foreach (Vector2 coord in GetComponent<CardBehaviour>().ConditionsShape)
            {
                if (board.GetSquare(cardCoord + coord) is not null)
                {
                    result.Add(board.GetSquare(cardCoord + coord));
                }
            }

            return result;
        }
    }

    # region condition

    /// <summary>
    /// 检查有多少特殊条件格子被占据
    /// </summary>
    /// <returns>被占据的特殊条件格子的数量</returns>
    public int GetSatisfiedSquaresCount()
    {
        if (!CardActing)
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
        else
        {
            return thisTurnSatisfiedSquaresCount;
        }
    }

    /// <summary>
    /// 是否有满足特殊条件
    /// </summary>
    public bool Conditioned{ get{ return CardActing ? thisTurnConditioned : GetSatisfiedSquaresCount() > 0;}}

    /// <summary>
    /// 获取满足特殊条件的卡牌
    /// </summary>
    /// <returns>满足特殊条件的卡牌</returns>
    public List<CardBehaviour> GetCardsSatisfiedCondition()
    {
        if (!CardActing)
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
        else
        {
            return thisTurnCardsSatisfiedCondition;
        }
    }

    #region card act value buffer

    int thisTurnSatisfiedSquaresCount;

    bool thisTurnConditioned;

    List<CardBehaviour> thisTurnCardsSatisfiedCondition;

    bool CardActing = false;

    public void SetConditionInfoWhenCardAct()
    {
        thisTurnSatisfiedSquaresCount = GetSatisfiedSquaresCount();
        print(GetComponent<CardBehaviour>().Id + " " + thisTurnSatisfiedSquaresCount);

        thisTurnConditioned = Conditioned;
        print(GetComponent<CardBehaviour>().Id + " " + thisTurnConditioned);

        thisTurnCardsSatisfiedCondition = GetCardsSatisfiedCondition();
        print(GetComponent<CardBehaviour>().Id + " " + thisTurnCardsSatisfiedCondition);

        CardActing = true;
    }

    public void ClearConditionInfo()
    {
        CardActing = false;
    }

    #endregion

    # endregion

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

    void Start()
    {
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ClearConditionInfo);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, ClearConditionInfo);
    }
}
