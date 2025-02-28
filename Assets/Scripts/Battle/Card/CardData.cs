using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    /// <summary>
    /// 卡牌的ID
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

    /// <summary>
    /// 卡牌名称
    /// </summary>
    public string CardName{ get; private set; }

    /// <summary>
    /// 卡面描述
    /// </summary>
    public string CardDescription{ get; private set; }

    /// <summary>
    /// 卡面图片
    /// </summary>
    public Sprite CardImage{ get; private set; }

    /// <summary>
    /// 卡牌背景框图片
    /// </summary>
    public Sprite CardBackground{ get; private set; }

    /// <summary>
    /// 方块模式的图片
    /// </summary>
    public Sprite BlockImage{ get; private set; }

    /// <summary>
    /// 卡牌的方块要组成的形状，通过一组向量表示每个方块相对原点的位置
    /// </summary>
    public List<Vector2> CardShape{ get; private set; }

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    public List<Vector2> ConditionsShape{ get; private set; }

    /// <summary>
    /// 卡牌的基础攻击力
    /// </summary>
    public int BaseDamage{ get; private set; }

    /// <summary>
    /// 卡牌的基础防御力
    /// </summary>
    public int BaseBlock{ get; private set; }

    /// <summary>
    /// 卡牌的基础特殊效果强度
    /// </summary>
    public int BaseEffect{ get; private set; }
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