using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 受到拖拽的卡牌Transform
    /// </summary>
    Transform cardRoot;

    /// <summary>
    /// 卡牌数据
    /// </summary>
    CardBehaviour cardData;

    /// <summary>
    /// 卡牌UI控制
    /// </summary>
    CardUI cardUI;

    /// <summary>
    /// 屏幕空间相机引用
    /// </summary>
    Camera mainCam;

    /// <summary>
    /// 板子
    /// </summary>
    BoardData boardData;

    /// <summary>
    /// 是否处于可拖拽状态
    /// </summary>
    public bool dragable = false;

    void Start()
    {
        mainCam = Camera.main;
        cardRoot = transform.parent.parent;
        cardData = cardRoot.GetComponent<CardBehaviour>();
        cardUI = cardRoot.GetComponent<CardUI>();
        boardData = PlayerBattleManager.Instance.board;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cardUI.UIState == UIStates.PLACED)
        {
            boardData.RemoveCard(cardData);
            cardData.ActOnRemoved();
        }
        cardUI.UIState = UIStates.DRAG;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragable)
        {
            Vector3 worldPos = mainCam.ScreenToWorldPoint(eventData.position);
            cardRoot.position = new Vector3(worldPos.x, worldPos.y, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (boardData.CanPlaceCard(cardRoot.GetComponent<CardBehaviour>()))
        {
            boardData.PlaceCard(cardRoot.GetComponent<CardBehaviour>());
            cardData.ActOnPlaced();
            cardUI.UIState = UIStates.PLACED;
        }
        else
        {
            cardUI.UIState = UIStates.HAND;
        }
    }
    
}
