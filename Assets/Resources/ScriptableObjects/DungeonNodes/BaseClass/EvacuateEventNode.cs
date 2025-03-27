using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvacuateEventNode", menuName = "ScriptableObject/DungeonNode/EvacuateEventNode")]
/// <summary>
/// 撤离节点
/// </summary>
public class EvacuateEventNode : EventNode
{
    /// <summary>
    /// 设置选项
    /// </summary>
    public void SetUpRoads()
    {
        EventChoice evacuateChoice = new EventChoice();
        evacuateChoice.choiceTypes.Add(EventChoiceType.NEW_NODE);
        evacuateChoice.nextNode = evacuateBattle;
        evacuateChoice.choiceText = evacuateBattle.nodeID;

        choices.Add(evacuateChoice);

        for (int i = 0; i < connectedNodes.Count; i++)
        {
            EventChoice newChoice = new EventChoice();
            newChoice.choiceTypes.Add(EventChoiceType.NEW_NODE);
            newChoice.nextNode = connectedNodes[i - 1];
            newChoice.choiceText = connectedNodes[i].nodeID;

            choices.Add(newChoice);
        }
    }

    /// <summary>
    /// 该撤离点导向的撤离战斗
    /// </summary>
    public EvacuateBattleNode evacuateBattle;
}
