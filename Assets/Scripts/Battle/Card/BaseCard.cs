using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    /// <summary>
    /// 卡牌相对于原点的绝对坐标
    /// </summary>
    public Vector2 AbsCoord{ get; private set; }

    /// <summary>
    /// 卡牌所在格子的坐标
    /// </summary>
    public Vector2 SquareCoord{ get; private set; }

    #region card data

    //bool exhausted = false;

    //int actAmount = 1;

    //bool useDefaultPosition = true;
    //Vector2 defaultPosition = Vector2.zero;

    //BaseCreature target = null;

    /// <summary>
    /// 卡牌的识别码
    /// </summary>
    public string Id{ get; private set; }

    /// <summary>
    /// 卡牌能力类型
    /// </summary>
    public CardAbilityType AbilityType{ get; private set; }

    /// <summary>
    /// 卡牌目标类型
    /// </summary>
    public CardTargetType TargetType{ get; private set; }


    #endregion











    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

/// <summary>
/// 卡牌能力的分类
/// </summary>
public enum CardAbilityType
{
    ATTACK,
    DEFEND,
    HEAL,
    SKILL,
    EXPAND,
    TRASH,
}

/// <summary>
/// 卡牌选取目标的分类
/// </summary>
public enum CardTargetType
{
    SELF,
    SINGLE_ENEMY,
    ALL_ENEMY,
}
