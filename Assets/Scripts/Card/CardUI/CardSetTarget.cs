using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSetTarget : MonoBehaviour
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
    /// 指示选择目标的箭头
    /// </summary>
    [SerializeField] DynamicCurve SelectTargetCurve;

    /// <summary>
    /// 选定目标后箭头的透明度
    /// </summary>
    [SerializeField] float ArrowAlphaAfterSelect = 0.4f;

    /// <summary>
    /// 遭遇战的怪物群组
    /// </summary>
    EnemyGroup enemyGroup;

    /// <summary>
    /// 屏幕空间相机引用
    /// </summary>
    Camera mainCam;

    /// <summary>
    /// 是否已经有了目标敌人
    /// </summary>
    bool HasTarget{ get{ return card.targetEnemy is not null; }}

    DynamicCurve currCurve;

    void Start()
    {
        mainCam = Camera.main;
        card = GetComponent<CardBehaviour>();
        targetType = card.TargetType;
        cardUI = GetComponent<CardUI>();
        enemyGroup = DungeonManager.Instance.battleManager.enemyGroup;

        cardUI.OnEnterHand += ClearArrow;
    }

    void OnDestroy()
    {
        cardUI.OnEnterHand -= ClearArrow;
    }

    void Update()
    {
        if (currCurve != null)
        {
            UpdateArrow();
            if (!HasTarget)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SetTarget();
                }
            }
        }
    }

    void SetTarget()
    {
        if (enemyGroup.GetHoveredEnemy() != null)
        {
            card.targetEnemy = enemyGroup.GetHoveredEnemy();
            SetArrowAlpha(ArrowAlphaAfterSelect);

            ActButton.Instance.button.interactable = true;
        }
    }

    #region arrow curve

    public void ShowArrow()
    {
        if (currCurve == null)
        {
            ActButton.Instance.button.interactable = false;

            currCurve = Instantiate(SelectTargetCurve);
            currCurve.transform.SetParent(transform, false);
            SetArrowAlpha(1);
            currCurve.transform.localScale *= 0.01f;
            currCurve.startPoint.position = transform.position;

            card.targetEnemy = null;
        }
        else
        {
            ClearArrow();
            ShowArrow();
        }
    }

    void UpdateArrow()
    {
        Vector3 position = Vector3.zero;
        if (!HasTarget)
        {
            position = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            position = card.targetEnemy.transform.position;
        }
        currCurve.endPoint.position = new Vector3(position.x, position.y, 10);
    }

    public void ClearArrow()
    {
        if (currCurve != null)
        {
            SetArrowAlpha(0);
            Destroy(currCurve);
            currCurve = null;
        }
    }

    void SetArrowAlpha(float alpha)
    {
        if (currCurve is not null)
        {
            currCurve.GetComponent<CanvasGroup>().alpha = alpha;
        }
    }

    #endregion
}
