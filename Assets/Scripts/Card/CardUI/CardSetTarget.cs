using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardSetTarget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 受到拖拽的卡牌Transform
    /// </summary>
    CardBehaviour card;

    /// <summary>
    /// 卡牌目标的类型
    /// </summary>
    CardTargetType targetType;

    /// <summary>
    /// 卡牌UI控制
    /// </summary>
    CardUI cardUI;

    /// <summary>
    /// 遭遇战的怪物群组
    /// </summary>
    EnemyGroup enemyGroup;

    /// <summary>
    /// 是否需要选择目标
    /// </summary>
    bool targetSettable{ get{ return cardUI.UIState == UIStates.SETTING_TARGET && targetType == CardTargetType.SINGLE_ENEMY;}}

    /// <summary>
    /// 拖拽之前的位置
    /// </summary>
    Vector3 originPosition;

    /// <summary>
    /// 屏幕空间相机引用
    /// </summary>
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        card = transform.parent.parent.GetComponent<CardBehaviour>();
        targetType = card.TargetType;
        cardUI = card.GetComponent<CardUI>();
        enemyGroup = DungeonManager.Instance.battleManager.enemyGroup;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPosition = card.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (targetSettable)
        {
            Vector3 worldPos = mainCam.ScreenToWorldPoint(eventData.position);
            card.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (targetSettable)
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
    }

    // public void OnDrawGizmos()
    // {
    //     if (targetSettable)
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    //     }
    // }
}
