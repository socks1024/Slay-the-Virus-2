using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BoardBehaviour : MonoBehaviour
{
    /// <summary>
    /// 游戏盘包含的所有格子
    /// </summary>
    Square[,] squares = new Square[5,5];

    /// <summary>
    /// 所有格子的列表
    /// </summary>
    public List<Square> AllSquares
    { 
        get
        {
           List<Square> s = new();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    s.Add(squares[i,j]);
                }
            }

            return s;
        }
    }

    /// <summary>
    /// 所有被锁定的格子的列表
    /// </summary>
    public List<Square> AllDisabledSquares
    {
        get
        {
            List<Square> s = new();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!squares[i,j].IsActive) s.Add(squares[i,j]);
                }
            }

            return s;
        }
    }

    public List<Square> AllEnabledSquares
    {
        get
        {
            List<Square> s = new();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (squares[i,j].IsActive) s.Add(squares[i,j]);
                }
            }

            return s;
        }
    }

    public List<Square> AllEmptySquares
    {
        get
        {
            List<Square> s = new();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (squares[i,j].IsActive && !squares[i,j].HasCard) s.Add(squares[i,j]);
                }
            }

            return s;
        }
    }

    /// <summary>
    /// 每个格子的大小
    /// </summary>
    const int squareSize = 25;

    /// <summary>
    /// 鼠标悬浮的格子
    /// </summary>
    public Square hoveredSquare{get { return GetHoveredSquare(); } }

    protected virtual void Start()
    {
        InitializeBoard();
        InitGetAllSquares();
        InitializeSquares(activateSquares);
        EventCenter.Instance.AddEventListener(EventType.ACT_START, OnActStart);

        InitializeSprites();
    }

    protected virtual void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.ACT_START, OnActStart);
    }

    # region Place cards

    /// <summary>
    /// 获取所有格子子物体并保存,x坐标为行，y坐标为列
    /// </summary>
    void InitGetAllSquares()
    {
        Square[] childSquares = GetComponentsInChildren<Square>();
        int index = 0;
        foreach (Square square in childSquares)
        {
            square.squareCoord = new Vector2(index / 5, index % 5);
            squares[index / 5, index % 5] = square;
            index++;
        }
    }

    /// <summary>
    /// 格子初始化
    /// </summary>
    public void InitializeSquares(bool[,] activateSquares)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                squares[j,i].IsActive = activateSquares[i,j];
                squares[j,i].CardData = null;
            }
        }
    }

    /// <summary>
    /// 获取格子
    /// </summary>
    /// <param name="coord">格子相对于左下角原点的坐标</param>
    /// <returns>目标格子,若超出范围则返回null</returns>
    public Square GetSquare(Vector2 coord)
    {
        if (coord.x < 0 || coord.x > 4 ||
            coord.y < 0 || coord.y > 4)
        {
            return null;
        }
        else
        {
            return squares[Mathf.RoundToInt(coord.x), Mathf.RoundToInt(coord.y)];
        }
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
    public Vector2 GetSquareCoord(Square targetSquare)
    {
        return targetSquare.squareCoord;
    }

    /// <summary>
    /// 获取鼠标下的格子
    /// </summary>
    /// <returns>鼠标下的格子，若没有的话返回null</returns>
    public Square GetHoveredSquare()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Square"))
            {
                return hit.collider.GetComponent<Square>();
            }
        }

        return null;
    }

    /// <summary>
    /// 判断卡牌能否被放置在对应的格子
    /// </summary>
    /// <param name="cardData">要放置的卡牌</param>
    /// <returns>判断结果</returns>
    public bool CanPlaceCard(CardBehaviour cardData)
    {
        if (hoveredSquare == null)
        {
            print("no hovered");
            return false;
        }
        
        foreach (Vector2 v in cardData.CardShape)
        {
            Square s = GetSquare(hoveredSquare,v);
            if (s == null)
            {
                print("no square");
                return false;
            }

            if (!s.IsActive)
            {
                print(s.squareCoord + " : inactive");
                return false;
            }

            if (s.HasCard)
            {
                print(s.squareCoord + " : has card " + s.CardData.Id);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 将卡牌放置在对应的格子上
    /// </summary>
    /// <param name="cardData">要放置的卡牌</param>
    public void PlaceCard(CardBehaviour cardData)
    {
        cardData.transform.position = hoveredSquare.transform.position;
        cardData.cardPosition.cardCoord = hoveredSquare.squareCoord;
        foreach (Vector2 v in cardData.CardShape)
        {
            Square s = GetSquare(hoveredSquare,v);
            s.CardData = cardData;
        }
        if (IsAllFilled())
        {
            //触发游戏盘的ActOnAllFilledTurnEnd回调
            ActOnAllFilledTurnEnd();
        }

        AudioManager.Instance.PlaySFX("PlaceCard");
    }

    /// <summary>
    /// 将某张卡牌从游戏盘上移除
    /// </summary>
    /// <param name="cardData">要移除的卡牌</param>
    public void RemoveCard(CardBehaviour cardData)
    {
        cardData.cardPosition.cardCoord = -Vector3.one;
        foreach (Square square in squares)
        {
            if (square.CardData == cardData)
            {
                //square.CardData.UIState = hand (or discard)
                square.CardData = null;
            }
        }

        AudioManager.Instance.PlaySFX("RemoveCard");
    }

    /// <summary>
    /// 将所有卡牌从游戏盘上移除
    /// </summary>
    public void ClearBoard()
    {
        foreach (CardBehaviour card in GetPlacedCards())
        {
            RemoveCard(card);
        }
    }

    /// <summary>
    /// 获取放在游戏盘上的所有卡牌
    /// </summary>
    /// <returns>所有卡牌</returns>
    public List<CardBehaviour> GetPlacedCards()
    {
        List<CardBehaviour> cards = new List<CardBehaviour>();

        foreach (Square square in squares)
        {
            if (square.HasCard)
            {
                bool repeated = false;

                foreach(CardBehaviour card in cards)
                {
                    if (square.CardData == card)
                    {
                        repeated = true;
                    }
                }

                if (!repeated)
                {
                    cards.Add(square.CardData);
                }
            }
        }

        return cards;
    }

    /// <summary>
    /// 获取被填充的格子数目
    /// </summary>
    /// <returns>被填充的格子数目</returns>
    public int GetFilledSquareCount()
    {
        int count = 0;
        foreach(Square square in squares)
        {
            if (square.HasCard) count++;
        }
        return count;
    }

    /// <summary>
    /// 检查整张游戏盘是否被填满
    /// </summary>
    /// <returns>是否被填满</returns>
    public bool IsAllFilled()
    {
        return GetFilledSquareCount() == 5 * 5;
    }

    /// <summary>
    /// 检查整张游戏盘是否为空
    /// </summary>
    /// <returns>是否为空</returns>
    public bool IsEmptyBoard()
    {
        return GetFilledSquareCount() == 0;
    }

    # endregion

    # region Play Cards

    /// <summary>
    /// 玩家按下ACT时触发的回调
    /// </summary>
    public void OnActStart()
    {
        GetPlacedCards().ForEach(card => {
            card.GetComponent<CardPosition>().SetConditionInfoWhenCardAct();
            card.GetComponent<CardSetTarget>().ClearArrow();
            cardActsAfterCardAct += card.ActAfterCardAct;
        });

        if (IsEmptyBoard())
        {
            TriggerCardActEnd();
        }
        
        CardsActBeforePlay();

        PlayAllCards();
    }

    /// <summary>
    /// 将所有卡牌调整应用到卡牌上
    /// </summary>
    void CardsActBeforePlay()
    {
        foreach (CardBehaviour card in GetPlacedCards())
        {
            card.ActBeforeCardAct();
        }
    }

    /// <summary>
    /// 一次性打出所有已经选择目标的卡牌
    /// </summary>
    public void PlayAllCards()
    {
        if (GetPlacedCards().Count == 0)
        {
            TriggerCardActEnd();
        }

        foreach (CardBehaviour card in GetPlacedCards())
        {
            PlayCard(card);
        }
    }

    UnityAction cardActsAfterCardAct;

    /// <summary>
    /// 打出一张卡牌
    /// </summary>
    /// <param name="card"></param>
    public void PlayCard(CardBehaviour card)
    {
        card.ActOnCardAct();
        if (card != null && !card.removedFromBattleAndDeck)
        {
            RemoveCard(card);
            card.ActOnRemoved();
            if (!card.exhausted) DungeonManager.Instance.battleManager.cardFlow.DiscardCard(card);
        }

        if (IsEmptyBoard())
        {
            TriggerCardActEnd();
        }
    }

    /// <summary>
    /// 所有卡牌都打完时触发
    /// </summary>
    public void TriggerCardActEnd()
    {
        cardActsAfterCardAct?.Invoke();
        cardActsAfterCardAct = null;
        
        ClearBoard();
        DungeonManager.Instance.battleManager.PlayAnimFinished = true;
    }

    # endregion

    /// <summary>
    /// 游戏盘的ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 游戏盘的默认初始格子启用情况
    /// </summary>
    protected bool[,] activateSquares = new bool[5,5]
    {
        {false,false,false,false,false},
        {false,true,true,true,false},
        {false,true,true,true,false},
        {false,true,true,true,false},
        {false,false,false,false,false},
    };

    /// <summary>
    /// 初始化游戏盘，在此为初始格子启用情况赋值
    /// </summary>
    protected abstract void InitializeBoard();

    /// <summary>
    /// 游戏盘的背景
    /// </summary>
    public Sprite background;

    /// <summary>
    /// 禁用时显示的图片
    /// </summary>
    public Sprite disabledSprite;

    /// <summary>
    /// 被填满时结束回合触发的效果
    /// </summary>
    public abstract void ActOnAllFilledTurnEnd();

    /// <summary>
    /// 填充棋盘的美术资源
    /// </summary>
    public void InitializeSprites()
    {
        GetComponent<Image>().sprite = background;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                squares[i,j].GetComponent<Image>().sprite = disabledSprite;
            }
        }
    }
}
