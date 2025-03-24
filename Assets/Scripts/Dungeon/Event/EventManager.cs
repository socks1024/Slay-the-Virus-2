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
    /// 设置事件显示
    /// </summary>
    /// <param name="description">事件描述</param>
    /// <param name="choices">事件选项列表</param>
    public void SetEvent(string description, List<EventChoice> choices)
    {
        eventDescription.text = description;
        for (int i = 0; i < choices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].choiceText;
            buttons[i].onClick.AddListener(choices[i].OnChoose);
        }
    }

    /// <summary>
    /// 设置事件显示
    /// </summary>
    /// <param name="eventInfo">事件信息</param>
    public void SetEvent(DungeonEventInfo eventInfo)
    {
        SetEvent(eventInfo.eventDescription, eventInfo.choices);
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
