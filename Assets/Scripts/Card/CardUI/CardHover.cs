using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 是否处于可悬浮预览状态
    /// </summary>
    public bool hoverable = false;

    /// <summary>
    /// 用于预览的卡牌
    /// </summary>
    GameObject previewCard;

    /// <summary>
    /// 弹出预览的时间延迟
    /// </summary>
    public float previewDelay = 1.0f;

    void Start()
    {
        previewCard = transform.parent.parent.gameObject;
    }

    /// <summary>
    /// 跟随光标展示卡牌牌面的预览
    /// </summary>
    /// <param name="eventData">EventSystem传入的鼠标事件</param>
    void ShowCard(PointerEventData eventData)
    {
        
    }

    /// <summary>
    /// 跟随光标展示卡牌形状的预览
    /// </summary>
    /// <param name="eventData">EventSystem传入的鼠标事件</param>
    void ShowBlocks(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 在给定的卡牌（处于卡牌状态）旁边展示该卡牌提到的关键词
    /// </summary>
    /// <param name="eventData">EventSystem传入的鼠标事件</param>
    /// <param name="cardUI">给定卡牌的CardUIState组件</param>
    void ShowKeywords(PointerEventData eventData, CardUI cardUI)
    {
        if (cardUI.Mode == CardMode.CARD)
        {

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    
}
