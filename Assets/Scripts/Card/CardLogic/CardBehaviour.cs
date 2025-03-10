using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CardPosition))]
[RequireComponent(typeof(CardUI))]
[RequireComponent(typeof(CardSwitchMode))]
[RequireComponent(typeof(TetrisAssembler))]
public abstract class CardBehaviour : MonoBehaviour
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
    public List<Vector2> CardShape{ get; set; }

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    public List<Vector2> ConditionsShape{ get; set; }

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

    #region CardActs

    [HideInInspector]
    /// <summary>
    /// 单个目标时瞄准的目标敌人
    /// </summary>
    public EnemyBehaviour targetEnemy;

    /// <summary>
    /// 卡牌的回合结束时效果
    /// </summary>
    public abstract void ActOnCardAct();

    /// <summary>
    /// 卡牌的填充时效果
    /// </summary>
    public abstract void ActOnPlaced();

    /// <summary>
    /// 卡牌的取下时效果
    /// </summary>
    public abstract void ActOnRemoved();

    /// <summary>
    /// 卡牌的被丢弃时效果
    /// </summary>
    public abstract void ActOnDiscard();

    #endregion

    protected virtual void Start()
    {
        CardShape = DeepCopy.DeepCopyValueTypeList<Vector2>(cardData.CardShape);
        ConditionsShape = DeepCopy.DeepCopyValueTypeList<Vector2>(cardData.ConditionsShape);
        //EventCenter.Instance.AddEventListener(EventType.TURN_END, ActOnTurnEnd);
    }

}

