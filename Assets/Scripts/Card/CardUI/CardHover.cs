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
    /// 受到拖拽的卡牌Transform
    /// </summary>
    Transform cardRoot;

    /// <summary>
    /// 用于预览的卡牌
    /// </summary>
    GameObject previewCard;

    /// <summary>
    /// 弹出预览的时间延迟
    /// </summary>
    public float previewDelay = 1.0f;

    /// <summary>
    /// 卡牌UI控制
    /// </summary>
    public CardUI cardUI;

    /// <summary>
    /// 手牌中的放大预览比例
    /// </summary>
    public float CardHandViewScale = 1.5f;

    void Start()
    {
        cardRoot = transform.parent.parent;
        previewCard = transform.parent.parent.gameObject;
        cardUI = cardRoot.GetComponent<CardUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(cardUI.UIState)
        {
            case UIStates.CUSTOM:
                break;
            case UIStates.HAND:
                // cardRoot.localScale = Vector3.one * 100 * CardHandViewScale;
                break;
            case UIStates.PLACED:
                break;
            case UIStates.SETTING_TARGET:
                break;
            case UIStates.DRAG:
                break;
            case UIStates.ANIMATE:
                break;
            case UIStates.SHOW_CARD:
                break;
            case UIStates.SHOW_DECK:
                break;
            case UIStates.BUTTON:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch(cardUI.UIState)
        {
            case UIStates.CUSTOM:
                break;
            case UIStates.HAND:
                // cardRoot.localScale = Vector3.one * 100;
                break;
            case UIStates.PLACED:
                break;
            case UIStates.SETTING_TARGET:
                break;
            case UIStates.DRAG:
                break;
            case UIStates.ANIMATE:
                break;
            case UIStates.SHOW_CARD:
                break;
            case UIStates.SHOW_DECK:
                break;
            case UIStates.BUTTON:
                break;
        }
    }

    
}
