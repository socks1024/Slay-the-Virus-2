using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region prop

    /// <summary>
    /// 受到拖拽的卡牌Transform
    /// </summary>
    Transform cardRoot;

    /// <summary>
    /// 卡牌数据
    /// </summary>
    CardBehaviour card;

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
    BoardBehaviour boardData{ get{ return DungeonManager.Instance.battleManager.board; }}

    /// <summary>
    /// 卡牌目标的类型
    /// </summary>
    CardTargetType targetType;

    /// <summary>
    /// 遭遇战的怪物群组
    /// </summary>
    EnemyGroup enemyGroup{get{return DungeonManager.Instance.battleManager.enemyGroup;}}

    /// <summary>
    /// 拖拽之前的位置
    /// </summary>
    Vector3 originPosition;

    CardRotate rotateComponent;

    public CardMode targetMode;

    #endregion

    void Start()
    {
        mainCam = Camera.main;
        cardUI = GetComponentInParent<CardUI>();
        cardRoot = cardUI.transform;
        card = cardRoot.GetComponent<CardBehaviour>();
        targetType = card.TargetType;
        rotateComponent = cardRoot.GetComponent<CardRotate>();
    }

    #region OnBeginDrag

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cardUI.Mode != targetMode) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            switch(cardUI.UIState)
            {
                case UIStates.CUSTOM:
                    break;
                case UIStates.HAND:
                    DungeonManager.Instance.battleManager.cardFlow.ReleaseCardFromHand(card);
                    break;
                case UIStates.PLACED:
                    if (!card.lockedOnBoard)
                    {
                        card.ActOnRemoved();
                        boardData.RemoveCard(card);
                        cardUI.UIState = UIStates.DRAG;
                    }
                    if (card.lockedOnBoard)
                    {
                        AudioManager.Instance.PlaySFX("DragLockedCard");
                    }
                    break;
                case UIStates.SETTING_TARGET:
                    originPosition = card.transform.position;
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

    #endregion

    #region OnDrag

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            switch(cardUI.UIState)
            {
                case UIStates.CUSTOM:
                    break;
                case UIStates.HAND:
                    break;
                case UIStates.PLACED:
                    break;
                case UIStates.SETTING_TARGET:
                    DragMove(eventData);
                    break;
                case UIStates.DRAG:
                    DragMove(eventData);
                    // if (Input.GetMouseButtonDown(1))
                    // {
                    //     rotateComponent.RotateCard(90);
                    // }
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

    void DragMove(PointerEventData eventData)
    {
        Vector3 worldPos = mainCam.ScreenToWorldPoint(eventData.position);
        cardRoot.position = new Vector3(worldPos.x, worldPos.y, 10);
    }

    #endregion

    #region OnEndDrag

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            switch(cardUI.UIState)
            {
                case UIStates.CUSTOM:
                    break;
                case UIStates.HAND:
                    break;
                case UIStates.PLACED:
                    break;
                case UIStates.SETTING_TARGET:
                    if (targetType == CardTargetType.SINGLE_ENEMY)
                    {
                        if (enemyGroup.GetHoveredEnemy() != null)
                        {
                            card.targetEnemy = enemyGroup.GetHoveredEnemy();
                            DungeonManager.Instance.battleManager.board.PlayCard(card);
                        }
                        else
                        {
                            card.transform.position = originPosition;
                        }
                    }
                    break;
                case UIStates.DRAG:
                    if (boardData.CanPlaceCard(cardRoot.GetComponent<CardBehaviour>()))
                    {
                        boardData.PlaceCard(cardRoot.GetComponent<CardBehaviour>());

                        // 开始选择目标
                        if (card.TargetType == CardTargetType.SINGLE_ENEMY)
                        {
                            cardUI.GetComponent<CardSetTarget>().ShowArrow();
                        }

                        card.ActOnPlaced();
                        cardUI.UIState = UIStates.PLACED;
                    }
                    else
                    {
                        DungeonManager.Instance.battleManager.cardFlow.AddCardToHand(card);
                    }
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

    #endregion
    
}
