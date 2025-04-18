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
    /// 卡牌行动类型
    /// </summary>
    public CardActType ActType{ get{return cardData.ActType;} }

    /// <summary>
    /// 卡牌条件类型
    /// </summary>
    public CardConditionType ConditionType{ get{return cardData.ConditionType;} }

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
    //[HideInInspector]
    public List<Vector2> CardShape;

    /// <summary>
    /// 能触发卡牌特效的格子，通过一组向量表示其相对原点的位置
    /// </summary>
    //[HideInInspector]
    public List<Vector2> ConditionsShape;

    /// <summary>
    /// 下一次打出卡牌的攻击力
    /// </summary>
    [HideInInspector]public int nextDamage;

    /// <summary>
    /// 下一次打出卡牌的防御力
    /// </summary>
    [HideInInspector]public int nextDefense;

    /// <summary>
    /// 下一次打出卡牌的治疗量
    /// </summary>
    [HideInInspector]public int nextHeal;

    /// <summary>
    /// 下一次打出卡牌的效果强度
    /// </summary>
    [HideInInspector]public int nextEffect;

    #endregion

    #region CardActs

    [HideInInspector]
    /// <summary>
    /// 单个目标时瞄准的目标敌人
    /// </summary>
    public EnemyBehaviour targetEnemy;

    protected PlayerBehaviour Player{ get{return DungeonManager.Instance.Player;} }

    [HideInInspector]
    /// <summary>
    /// 与卡牌位置相关逻辑的处理组件
    /// </summary>
    public CardPosition cardPosition;

    [HideInInspector]
    /// <summary>
    /// 是否被锁定在板子上
    /// </summary>
    public bool lockedOnBoard;

    [HideInInspector]
    public CardPile currPile;

    [HideInInspector]
    /// <summary>
    /// 是否被消耗
    /// </summary>
    public bool exhausted = false;

    [HideInInspector]
    /// <summary>
    /// 是否被摧毁
    /// </summary>
    public bool removedFromBattleAndDeck = false;

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

    protected virtual void Awake()
    {
        cardPosition = GetComponent<CardPosition>();
        
        CardShape = DeepCopy.DeepCopyValueTypeList<Vector2>(cardData.CardShape);
        ConditionsShape = DeepCopy.DeepCopyValueTypeList<Vector2>(cardData.ConditionsShape);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, ResetData);

        nextDamage = cardData.BaseDamage;
        nextDefense = cardData.BaseDefense;
        nextHeal = cardData.BaseHeal;
        nextEffect = cardData.BaseEffect;
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, ResetData);
    }

    /// <summary>
    /// 刷新卡牌的数据，每回合开始时调用
    /// </summary>
    protected virtual void ResetData()
    {
        nextDamage = cardData.BaseDamage;
        nextDefense = cardData.BaseDefense;
        nextHeal = cardData.BaseHeal;
        nextEffect = cardData.BaseEffect;

        lockedOnBoard = false;
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     CardShape.ForEach(shape => {
    //         Vector3 vec = transform.position;
    //         vec.x += shape.x;
    //         vec.y += shape.y;
    //         Gizmos.DrawCube(vec, Vector3.one);
    //     });
    // }

}

