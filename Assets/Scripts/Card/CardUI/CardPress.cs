using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// 是否处于可点击状态
    /// </summary>
    public bool pressable = false;

    /// <summary>
    /// 按下时会改变的图片
    /// </summary>
    Image cardImg;

    /// <summary>
    /// 被点击后触发的回调
    /// </summary>
    public UnityAction callback;

    /// <summary>
    /// 卡牌的UI控制组件
    /// </summary>
    CardUI cardUI;

    void Start()
    {
        cardImg = GetComponent<Image>();
        cardUI = transform.parent.parent.GetComponent<CardUI>();
        callback += cardUI.OnPress;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressable)
        {
            cardImg.color = Color.gray;
            callback?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(pressable)
        {
            cardImg.color = Color.white;
        }
    }
}
