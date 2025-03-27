using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    /// <summary>
    /// 事件的描述性文字
    /// </summary>
    [SerializeField] TextMeshProUGUI eventDescription;

    /// <summary>
    /// 事件的标题
    /// </summary>
    [SerializeField] TextMeshProUGUI titleText;

    /// <summary>
    /// 所有事件响应按钮的根物体
    /// </summary>
    [SerializeField] GameObject buttonsRoot;

    /// <summary>
    /// 所有事件响应按钮的根物体
    /// </summary>
    [SerializeField] Image image;

    /// <summary>
    /// 所有的事件响应按钮
    /// </summary>
    Button[] buttons{ get{ return buttonsRoot.GetComponentsInChildren<Button>(); }}

    /// <summary>
    /// 当前事件
    /// </summary>
    [HideInInspector]public DungeonNode nextNode;

    /// <summary>
    /// 设置常规事件
    /// </summary>
    /// <param name="node">节点</param>
    void SetNormalEvent(DungeonNode node)
    {
        titleText.text = (node.nodeInfo as EventNodeInfo).title;
        eventDescription.text = (node.nodeInfo as EventNodeInfo).eventDescription;
        image.sprite = (node.nodeInfo as EventNodeInfo).sprite;
        
        for (int i = 0; i < (node.nodeInfo as EventNodeInfo).choices.Count; i++)
        {
            SetButton(i,(node.nodeInfo as EventNodeInfo).choices[i].choiceText,(node.nodeInfo as EventNodeInfo).choices[i].OnChoose);
        }
    }

    /// <summary>
    /// 设置事件显示
    /// </summary>
    /// <param name="node">事件信息</param>
    public void SetEvent(DungeonNode node)
    {
        nextNode = node;

        if (node.nodeInfo is EvacuateEventNodeInfo)
        {
            GenerateEvacuateEventChoices(node);
        }
        else
        {
            SetNormalEvent(node);
        }
    }

    /// <summary>
    /// 设置撤离点事件显示
    /// </summary>
    /// <param name="node">撤离点信息</param>
    void GenerateEvacuateEventChoices(DungeonNode node)
    {
        EvacuateEventNodeInfo info = node.nodeInfo as EvacuateEventNodeInfo;

        titleText.text = info.title;
        eventDescription.text = info.eventDescription;
        image.sprite = info.sprite;

        SetButton(0, info.evacuateButtonText, () => {
            DungeonManager.Instance.eventManager.nextNode = DungeonNodeLib.GetNode(info.evacuateBattle.nodeID);
        });
    }

    /// <summary>
    /// 启用一个按钮
    /// </summary>
    /// <param name="index">按钮的序号</param>
    /// <param name="text">按钮的文本</param>
    /// <param name="action">按下按钮时触发的事件</param>
    void SetButton(int index, string text, UnityAction action)
    {
        buttons[index].gameObject.SetActive(true);
        buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = text;
        buttons[index].onClick.AddListener(action);
        buttons[index].onClick.AddListener(EndEvent);
    }

    /// <summary>
    /// 结束事件
    /// </summary>
    void EndEvent()
    {
        ClearEvent();
        DungeonManager.Instance.EnterNode(nextNode);
    }

    /// <summary>
    /// 清空事件显示
    /// </summary>
    public void ClearEvent()
    {
        eventDescription.text = "";
        foreach (Button button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.onClick = null;
            button.gameObject.SetActive(false);
        }
    }

    void Awake()
    {
        ClearEvent();
    }
}
