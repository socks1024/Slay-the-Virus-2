using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    /// <summary>
    /// 事件的描述性文字
    /// </summary>
    [SerializeField] TextMeshProUGUI eventDescription;

    /// <summary>
    /// 所有事件响应按钮的根物体
    /// </summary>
    [SerializeField] GameObject buttonsRoot;

    /// <summary>
    /// 所有的事件响应按钮
    /// </summary>
    Button[] buttons{ get{ return buttonsRoot.GetComponentsInChildren<Button>(); }}

    /// <summary>
    /// 当前事件
    /// </summary>
    EventNode node;

    /// <summary>
    /// 设置常规事件
    /// </summary>
    /// <param name="node">节点</param>
    void SetNormalEvent(EventNode node)
    {
        eventDescription.text = node.eventDescription;
        for (int i = 0; i < node.choices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = node.choices[i].choiceText;
            buttons[i].onClick.AddListener(node.choices[i].OnChoose);
            buttons[i].onClick.AddListener(EndEvent);
        }
    }

    /// <summary>
    /// 设置事件显示
    /// </summary>
    /// <param name="eventInfo">事件信息</param>
    public void SetEvent(EventNode eventInfo)
    {
        node = eventInfo;

        if (eventInfo is EvacuateNode)
        {
            (eventInfo as EvacuateNode).SetUpRoads();
        }
        SetNormalEvent(eventInfo);
    }

    /// <summary>
    /// 结束事件
    /// </summary>
    void EndEvent()
    {
        if (node is not EvacuateNode)
        {
            DungeonManager.Instance.EnterNode(node.connectedNodes[0]);
        }
        else
        {
            DungeonManager.Instance.EnterNode((node as EvacuateNode).nextNode);
        }
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
