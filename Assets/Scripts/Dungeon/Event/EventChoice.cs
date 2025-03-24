using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct EventChoice
{
    /// <summary>
    /// 选项的描述性文本
    /// </summary>
    public string choiceText;

    /// <summary>
    /// 被按下时触发的回调
    /// </summary>
    public UnityAction OnChoose;
}
