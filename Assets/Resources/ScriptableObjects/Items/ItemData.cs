using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    /// <summary>
    /// 道具的ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 检视道具时显示的名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 检视道具时显示的描述
    /// </summary>
    public string Description;
}
