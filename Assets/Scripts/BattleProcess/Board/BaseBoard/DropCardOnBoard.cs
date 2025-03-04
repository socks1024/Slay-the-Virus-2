using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCardOnBoard : MonoBehaviour
{
    /// <summary>
    /// 板子引用
    /// </summary>
    BoardData board;

    GraphicRaycaster gr;

    EventSystem es;

    void Start()
    {
        board = GetComponentInParent<BoardData>();
    }

    public void Show()
    {
        print("enter");
    }

    
}
