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

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    
}
