using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObject/BuffData", order = 0)]
public class BuffData : ScriptableObject
{
    /// <summary>
    /// 身份识别
    /// </summary>
    public string ID;

    /// <summary>
    /// Buff的名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 状态效果的图片
    /// </summary>
    public Sprite BuffSprite;

    /// <summary>
    /// 状态效果的类型
    /// </summary>
    public BuffType Type;
}

public enum BuffType
{
    POSITIVE,
    NEGATIVE,
}
