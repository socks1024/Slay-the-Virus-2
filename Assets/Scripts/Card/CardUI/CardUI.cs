using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    CardMode mode;

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
            uiState = value;
            switch(uiState)
            {
                case UIStates.CUSTOM:
                    break;
                case UIStates.HAND:
                    transform.localScale = new Vector3(100, 100, 100);
                    GetComponent<CardRotate>().Homing();
                    SetAllUIPropPrivate(CardMode.CARD,true,false,true);
                    break;
                case UIStates.PLACED:
                    SetAllUIPropPrivate(CardMode.BLOCKS,true,false,true);
                    break;
                case UIStates.SETTING_TARGET:
                    SetAllUIPropPrivate(CardMode.BLOCKS,false,false,true);
                    break;
                case UIStates.DRAG:
                    SetAllUIPropPrivate(CardMode.BLOCKS,true,false,false);
                    break;
                case UIStates.ANIMATE:
                    SetAllUIPropPrivate(CardMode.CARD,false,false,false);
                    break;
                case UIStates.SHOW_CARD:
                    SetAllUIPropPrivate(CardMode.CARD,false,false,true);
                    break;
                case UIStates.SHOW_DECK:
                    SetAllUIPropPrivate(CardMode.BLOCKS,false,false,true);
                    break;
                case UIStates.BUTTON:
                    SetAllUIPropPrivate(CardMode.CARD,false,true,true);
                    break;
            }
        }
    }
    UIStates uiState = UIStates.ANIMATE;

    /// <summary>
    /// 按下时回调
    /// </summary>
    public UnityAction OnPress;

    /// <summary>
    /// 卡牌名字的显示组件
    /// </summary>
    public TextMeshProUGUI nameText;

    /// <summary>
    /// 卡牌描述的显示组件
    /// </summary>
    public TextMeshProUGUI descriptionText;

    /// <summary>
    /// 卡牌的背景
    /// </summary>
    public Image cardBG;

    /// <summary>
    /// 卡牌的稀有度框
    /// </summary>
    public Image rarityFrame;

    /// <summary>
    /// 卡面配图
    /// </summary>
    public Image cardImage;

    #region card art

    [Header("Card Background")]
    public Sprite AttackBG;
    public Sprite DefenseBG;
    public Sprite HealBG;
    public Sprite SkillBG;
    public Sprite ExpandBG;
    public Sprite TrashBG;

    [Header("Card Rarity Frame")]
    public Sprite CommonFrame;
    public Sprite UncommonFrame;
    public Sprite RareFrame;
    public Sprite UniqueFrame;
    public Sprite TrashFrame;

    #endregion

    void Start()
    {
        cardBehaviour = GetComponent<CardBehaviour>();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            t.gameObject.SetActive(true);
        }
        Mode = CardMode.CARD;
        EventCenter.Instance.AddEventListener(EventType.ACT_START, OnCardAct);
        SetCardUI();
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.ACT_START, OnCardAct);
    }

    /// <summary>
    /// 快速设置所有UI属性
    /// </summary>
    /// <param name="cardMode">卡牌所处图形状态</param>
    /// <param name="dragable">能否拖拽</param>
    /// <param name="pressable">能否点击</param>
    /// <param name="hoverable">能否悬浮预览</param>
    void SetAllUIPropPrivate(CardMode cardMode, bool dragable, bool pressable, bool hoverable)
    {
        Mode = cardMode;
        SetDragable(dragable);
        SetPressable(pressable);
        SetHoverPreview(hoverable);
    }

    /// <summary>
    /// 从外部快速设置所有UI属性
    /// </summary>
    /// <param name="cardMode">卡牌所处图形状态</param>
    /// <param name="dragable">能否拖拽</param>
    /// <param name="pressable">能否点击</param>
    /// <param name="hoverable">能否悬浮预览</param>
    public void SetAllUIProp(CardMode cardMode, bool dragable, bool pressable, bool hoverable)
    {
        if (UIState == UIStates.CUSTOM)
        {
            SetAllUIPropPrivate(cardMode,dragable,pressable,hoverable);
        }
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
    void OnCardAct()
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

    /// <summary>
    /// 显示卡牌的基础UI信息
    /// </summary>
    void SetCardUI()
    {
        CardData data = cardBehaviour.cardData;
        //nameText.text = data.;
        //descriptionText.text = data.;
        
        if (cardImage != null)
        {
            cardImage.sprite = data.CardTex;
        }

        switch (data.AbilityType)
        {
            case CardAbilityType.ATTACK:
                cardBG.sprite = AttackBG;
                break;
            case CardAbilityType.DEFEND:
                cardBG.sprite = DefenseBG;
                break;
            case CardAbilityType.HEAL:
                cardBG.sprite = HealBG;
                break;
            case CardAbilityType.SKILL:
                cardBG.sprite = SkillBG;
                break;
            case CardAbilityType.EXPAND:
                cardBG.sprite = ExpandBG;
                break;
            case CardAbilityType.TRASH:
                cardBG.sprite = TrashBG;
                break;
        }

        switch(data.RarityType)
        {
            case CardRarityType.COMMON:
                rarityFrame.sprite = CommonFrame;
                break;
            case CardRarityType.UNCOMMON:
                rarityFrame.sprite = UncommonFrame;
                break;
            case CardRarityType.RARE:
                rarityFrame.sprite = RareFrame;
                break;
            case CardRarityType.UNIQUE:
                rarityFrame.sprite = UniqueFrame;
                break;
            case CardRarityType.TRASH:
                rarityFrame.sprite = TrashFrame;
                break;
        }
    }

}

/// <summary>
/// 卡牌所有交互模式的枚举
/// </summary>
public enum UIStates
{
    CUSTOM,
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
    /// <summary>
    /// 显示为卡牌的外形
    /// </summary>
    CARD,

    /// <summary>
    /// 显示为方块的外形
    /// </summary>
    BLOCKS,
}
