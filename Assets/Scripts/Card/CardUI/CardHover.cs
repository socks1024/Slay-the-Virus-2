using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    /// 卡牌UI控制
    /// </summary>
    CardUI cardUI;

    /// <summary>
    /// 手牌中的放大预览比例
    /// </summary>
    public float CardHandViewScale = 1.5f;

    /// <summary>
    /// 手牌中的放大预览上浮距离
    /// </summary>
    public float CardHandViewOffset = 4;

    /// <summary>
    /// 手牌中的放大预览动画时间
    /// </summary>
    public float CardHandViewTime = 0.1f;

    bool isHandPreviewing = false;

    Vector3 originalPos;

    void Start()
    {
        cardRoot = transform.parent.parent;
        cardUI = cardRoot.GetComponent<CardUI>();
        cardUI.OnLeaveHand += ReturnHandPreview;
    }

    void OnDestroy()
    {
        // cardUI.OnLeaveHand -= ReturnHandPreview;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(cardUI.UIState)
        {
            case UIStates.CUSTOM:
                break;
            case UIStates.HAND:
                if (!eventData.dragging) EnterHandPreview();
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

    public void EnterHandPreview()
    {
        if (!isHandPreviewing)
        {
            cardRoot.DOComplete();

            cardRoot.DOScale(cardUI.baseScale * CardHandViewScale, CardHandViewTime);

            originalPos = cardRoot.position;

            Vector3 vec = cardRoot.position;
            vec.y += CardHandViewOffset;

            cardRoot.DOMove(vec, CardHandViewTime);

            cardRoot.SetAsLastSibling();

            isHandPreviewing = true;
        }
    }

    public void LeaveHandPreview()
    {
        if (isHandPreviewing)
        {
            cardRoot.DOComplete();

            cardRoot.DOScale(cardUI.baseScale, CardHandViewTime);

            cardRoot.DOMove(originalPos, CardHandViewTime);

            isHandPreviewing = false;
        }
    }

    public void ReturnHandPreview()
    {
        if (isHandPreviewing)
        {
            cardRoot.DOKill();

            cardRoot.DOScale(cardUI.baseScale, CardHandViewTime);

            cardRoot.DOMove(originalPos, CardHandViewTime);

            isHandPreviewing = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeaveHandPreview();
        switch(cardUI.UIState)
        {
            case UIStates.CUSTOM:
                break;
            case UIStates.HAND:
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
