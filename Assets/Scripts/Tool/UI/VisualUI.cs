using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class VisualUI
{
    /// <summary>
    /// 使UI物体隐藏或显示为原状,不影响UI交互
    /// </summary>
    /// <param name="gameObject">自身及子物体中带有UI物体的物体</param>
    /// <param name="visible">是否可见</param>
    public static void SetVisible(GameObject gameObject, bool visible)
    {
        foreach (Image img in gameObject.GetComponentsInChildren<Image>())
        {
            if (visible)
            {
                img.color = Color.white;
            }
            else
            {
                img.color = new Color(0,0,0,0);
            }
        }

        foreach (TextMeshProUGUI tmp in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (visible)
            {
                tmp.color = Color.white;
            }
            else
            {
                tmp.color = new Color(0,0,0,0);
            }
        }
    }
}
