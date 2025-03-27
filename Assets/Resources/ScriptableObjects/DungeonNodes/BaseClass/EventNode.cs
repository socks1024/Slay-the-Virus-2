using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventNode", menuName = "ScriptableObject/DungeonNode/EventNode")]
/// <summary>
/// 事件节点
/// </summary>
public class EventNode : DungeonNode
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
    /// 该选项会触发的选项操作类型
    /// </summary>
    public List<EventChoiceType> choiceTypes;

    #region Choice Parameters

    [Header("选项参数")]

    public int moneyVariation;

    public int healthVariation;

    public RelicBehaviour p_Relic;

    public BoardBehaviour p_Board;

    public DungeonNode nextNode;

    #endregion

    /// <summary>
    /// 选择选项会触发的行动
    /// </summary>
    public void OnChoose()
    {
        foreach (var choice in choiceTypes)
        {
            switch (choice)
            {
                case EventChoiceType.MONEY:
                    ActionLib.PlayerChangeMoney(moneyVariation);
                    break;
                case EventChoiceType.HEALTH_CHANGE:
                    ActionLib.DirectlyChangeHealthAction(DungeonManager.Instance.Player, healthVariation);
                    break;
                case EventChoiceType.RELIC_LOOT:
                    ActionLib.PlayerGainRelic(p_Relic);
                    break;
                case EventChoiceType.CARD_LOOT:
                    //
                    break;
                case EventChoiceType.BOARD_LOOT:
                    //
                    break;
                case EventChoiceType.NEW_NODE:
                    DungeonManager.Instance.eventManager.nextNode = nextNode;
                    break;
            }
        }
    }
}

public enum EventChoiceType
{
    MONEY,
    CARD_LOOT,
    RELIC_LOOT,
    BOARD_LOOT,
    NEW_NODE,
    HEALTH_CHANGE,
}

