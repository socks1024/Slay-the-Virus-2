using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    /// <summary>
    /// 卡牌现在处于牌模式还是方块模式
    /// </summary>
    public CardMode Mode
    {
        get { return mode; }
        private set 
        {
            mode = value;
            switch (mode)
            {
                case CardMode.CARD:
                    GetComponent<CardSwitchMode>()?.SwitchToCardMode();
                    break;
                case CardMode.BLOCKS:
                    GetComponent<CardSwitchMode>()?.SwitchToBlockMode();
                    break;
            }
        }
    }
    CardMode mode = CardMode.CARD;

    /// <summary>
    /// 卡牌引用
    /// </summary>
    CardBehaviour cardBehaviour;

    /// <summary>
    /// 卡牌当前的UI交互模式
    /// </summary>
    public UIStates UIState
    {
        get { return uiState; }
        set
        {
            switch(uiState)
            {
                case UIStates.HAND:
                    BattleManager.Instance.cardFlow.ReleaseCardFromHand(cardBehaviour);
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
            uiState = value;
            switch(uiState)
            {
                case UIStates.HAND:
                    BattleManager.Instance.cardFlow.AddCardToHand(cardBehaviour);
                    GetComponent<CardRotate>().Homing();
                    SetAllUIProp(CardMode.CARD,true,false,true);
                    break;
                case UIStates.PLACED:
                    SetAllUIProp(CardMode.BLOCKS,true,false,true);
                    break;
                case UIStates.SETTING_TARGET:
                    SetAllUIProp(CardMode.BLOCKS,false,false,true);
                    break;
                case UIStates.DRAG:
                    SetAllUIProp(CardMode.BLOCKS,true,false,false);
                    break;
                case UIStates.ANIMATE:
                    SetAllUIProp(CardMode.CARD,false,false,false);
                    break;
                case UIStates.SHOW_CARD:
                    SetAllUIProp(CardMode.CARD,false,false,true);
                    break;
                case UIStates.SHOW_DECK:
                    SetAllUIProp(CardMode.BLOCKS,false,false,true);
                    break;
                case UIStates.BUTTON:
                    SetAllUIProp(CardMode.CARD,false,true,true);
                    break;
            }
        }
    }
    UIStates uiState = UIStates.ANIMATE;

    void Start()
    {
        cardBehaviour = GetComponent<CardBehaviour>();
        UIState = UIStates.HAND;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            t.gameObject.SetActive(true);
        }
        EventCenter.Instance.AddEventListener(EventType.TURN_END, OnTurnEnd);
    }

    /// <summary>
    /// 快速设置所有UI属性
    /// </summary>
    /// <param name="cardMode">卡牌所处图形状态</param>
    /// <param name="dragable">能否拖拽</param>
    /// <param name="pressable">能否点击</param>
    /// <param name="hoverable">能否悬浮预览</param>
    void SetAllUIProp(CardMode cardMode, bool dragable, bool pressable, bool hoverable)
    {
        Mode = cardMode;
        SetDragable(dragable);
        SetPressable(pressable);
        SetHoverPreview(hoverable);
    }

    /// <summary>
    /// 设置卡牌的可拖拽性
    /// </summary>
    /// <param name="dragable">可拖拽性</param>
    void SetDragable(bool dragable)
    {
        foreach (CardDrag cardDrag in GetComponentsInChildren<CardDrag>())
        {
            cardDrag.dragable = dragable;
        }
        
    }

    /// <summary>
    /// 设置卡牌的可点击性
    /// </summary>
    /// <param name="pressable">可点击性</param>
    void SetPressable(bool pressable)
    {
        foreach (CardPress cardPress in GetComponentsInChildren<CardPress>())
        {
            cardPress.pressable = pressable;
        }
    }

    /// <summary>
    /// 设置卡牌能否悬浮预览
    /// </summary>
    /// <param name="hoverable">能否悬浮预览</param>
    void SetHoverPreview(bool hoverable)
    {
        foreach (CardHover cardHover in GetComponentsInChildren<CardHover>())
        {
            cardHover.hoverable = hoverable;
        }
    }

    /// <summary>
    /// 玩家按下回合结束按钮时自动切换卡牌模式
    /// </summary>
    void OnTurnEnd()
    {
        if (UIState == UIStates.PLACED)
        {
            UIState = UIStates.SETTING_TARGET;
        }
        else
        {
            UIState = UIStates.ANIMATE;
        }
    }

}

/// <summary>
/// 卡牌所有交互模式的枚举
/// </summary>
public enum UIStates
{
    HAND,//CARD,DRAGABLE,HOVER_PREVIEW
    PLACED,//BLOCK,DRAGABLE,HOVER_PREVIEW
    SETTING_TARGET,//BLOCK,HOVER_PREVIEW,SET_TARGET
    DRAG,//BLOCK,DRAGABLE
    ANIMATE,//CARD
    SHOW_CARD,//CARD,HOVER_PREVIEW
    SHOW_DECK,//BLOCK,HOVER_PREVIEW
    BUTTON,//CARD,CLICKABLE,HOVER_PREVIEW
}

public enum CardMode
{
    CARD,
    BLOCKS,
}
