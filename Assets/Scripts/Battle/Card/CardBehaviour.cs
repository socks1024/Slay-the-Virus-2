using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    #region CardDataReference

    /// <summary>
    /// 卡牌的数据资源
    /// </summary>
    public CardData cardData;

    /// <summary>
    /// 卡牌的ID
    /// </summary>
    public string Id{ get {return cardData.Id;} }

    /// <summary>
    /// 卡牌能力类型
    /// </summary>
    public CardAbilityType AbilityType{ get{return cardData.AbilityType;} }

    /// <summary>
    /// 卡牌目标类型
    /// </summary>
    public CardTargetType TargetType{ get{return cardData.TargetType;} }

    /// <summary>
    /// 卡牌稀有度类型
    /// </summary>
    public CardRarityType RarityType{ get{return cardData.RarityType;} }

    /// <summary>
    /// 卡牌的方块要组成的形状，通过一组向量表示每个方块相对原点的位置
    /// </summary>
    public List<Vector2> CardShape{ get{return cardData.CardShape;} }

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    public List<Vector2> ConditionsShape{ get{return cardData.ConditionsShape;} }

    /// <summary>
    /// 卡牌的基础攻击力
    /// </summary>
    public int BaseDamage{ get{return cardData.BaseDamage;} }

    /// <summary>
    /// 卡牌的基础防御力
    /// </summary>
    public int BaseDefense{ get{return cardData.BaseDefense;} }

    /// <summary>
    /// 卡牌的基础特殊效果强度
    /// </summary>
    public int BaseEffect{ get{return cardData.BaseEffect;} }

    #endregion
}

