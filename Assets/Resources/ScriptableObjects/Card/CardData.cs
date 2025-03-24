using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObject/CardData", order = 0)]
public class CardData : ScriptableObject 
{
    /// <summary>
    /// 卡牌的ID
    /// </summary>
    public string Id;

    /// <summary>
    /// 卡牌能力类型
    /// </summary>
    public CardAbilityType AbilityType;

    /// <summary>
    /// 卡牌目标类型
    /// </summary>
    public CardTargetType TargetType;

    /// <summary>
    /// 卡牌稀有度类型
    /// </summary>
    public CardRarityType RarityType;

    /// <summary>
    /// 卡牌的方块要组成的形状，通过一组向量表示每个方块相对原点的位置
    /// </summary>
    public List<Vector2> CardShape;

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    public List<Vector2> ConditionsShape;

    /// <summary>
    /// 卡牌的基础攻击力
    /// </summary>
    public int BaseDamage;

    /// <summary>
    /// 卡牌的基础防御力
    /// </summary>
    public int BaseDefense;

    /// <summary>
    /// 卡牌的基础治疗量
    /// </summary>
    public int BaseHeal;

    /// <summary>
    /// 卡牌的基础特殊效果强度
    /// </summary>
    public int BaseEffect;

    // /// <summary>
    // /// 卡牌名称
    // /// </summary>
    // public string Name;

    // /// <summary>
    // /// 卡面描述
    // /// </summary>
    // public string Description;

    /// <summary>
    /// 卡面配图
    /// </summary>
    public Sprite CardTex;

    /// <summary>
    /// 方块配图
    /// </summary>
    public Sprite BlockTex;
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

/// <summary>
/// 卡牌稀有度的分类
/// </summary>
public enum CardRarityType
{
    COMMON,
    UNCOMMON,
    RARE,
    UNIQUE,
    TRASH,
}
