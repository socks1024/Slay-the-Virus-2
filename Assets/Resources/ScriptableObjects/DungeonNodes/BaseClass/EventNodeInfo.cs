using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "EventNodeInfo", menuName = "ScriptableObject/DungeonNodeInfo/EventNodeInfo")]
/// <summary>
/// 事件节点
/// </summary>
public class EventNodeInfo : DungeonNodeInfo
{
    /// <summary>
    /// 事件标题
    /// </summary>
    public string title;

    /// <summary>
    /// 事件的配图1：1
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// 事件描述
    /// </summary>
    [TextArea]
    public string eventDescription;

    /// <summary>
    /// 事件选项
    /// </summary>
    public List<EventChoice> choices;
}

[Serializable]
public class EventChoice
{
    /// <summary>
    /// 选项的描述性文本
    /// </summary>
    [TextArea]
    public string choiceText;

    /// <summary>
    /// 该选项会触发的选项操作类型及每个类型触发的概率
    /// </summary>
    public SerializableDictionary<EventChoiceType,float> choiceTypes;

    #region Choice Parameters

    [Header("选项参数")]

    public int moneyVariation;

    public int healthVariation;

    public RelicBehaviour p_Relic;

    public CardBehaviour p_Card;

    public BoardBehaviour p_Board;

    public DungeonNodeInfo nextNodeInfo;

    public bool repeatable;

    #endregion

    public bool Available
    { 
        get
        {
            if (choiceTypes.Contains(EventChoiceType.MONEY))
            {
                if (DungeonManager.Instance.Player.Nutrition < moneyVariation)
                {
                    return false;
                }
            }

            if (choiceTypes.Contains(EventChoiceType.HEALTH_CHANGE))
            {
                if (DungeonManager.Instance.Player.takeDamage.Health + healthVariation <= 0)
                {
                    return false;
                }
            }

            return true; 
        } 
    }

    /// <summary>
    /// 选择选项会触发的行动
    /// </summary>
    public void OnChoose()
    {
        foreach (var choice in choiceTypes)
        {
            switch (choice.Key)
            {
                case EventChoiceType.MONEY:
                    if(Random.value < choice.Value) ActionLib.PlayerChangeMoney(moneyVariation);
                    break;
                case EventChoiceType.HEALTH_CHANGE:
                    if(Random.value < choice.Value) ActionLib.DirectlyChangeHealthAction(DungeonManager.Instance.Player, healthVariation);
                    break;
                case EventChoiceType.RELIC_LOOT:
                    if(Random.value < choice.Value) ActionLib.PlayerGainRelic(p_Relic);
                    break;
                case EventChoiceType.CARD_LOOT:
                    if(Random.value < choice.Value) ActionLib.PlayerAddCardToDeck(p_Card);
                    break;
                case EventChoiceType.PACK_LOOT:
                    break;
                case EventChoiceType.BOARD_LOOT:
                    break;
                // case EventChoiceType.NEW_NODE:
                //     DungeonNode node = DungeonNodeLib.GetNode(nextNodeInfo.nodeID);
                //     node.connectedNodes.Add();
                //     if(Random.value < choice.Value) DungeonManager.Instance.eventManager.currNode = ;
                //     break;
            }
        }
    }
}

public enum EventChoiceType
{
    MONEY,
    CARD_LOOT,
    RELIC_LOOT,
    PACK_LOOT,
    BOARD_LOOT,
    // NEW_NODE,
    HEALTH_CHANGE,
}

