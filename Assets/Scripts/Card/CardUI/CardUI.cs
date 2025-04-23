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
                    transform.localScale = Vector3.one * 100;
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
                    transform.localScale = Vector3.one * 100;
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
                    transform.SetAsLastSibling();
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
    /// 卡牌的外边框
    /// </summary>
    public Image cardBGF;

    /// <summary>
    /// 卡牌的稀有度条
    /// </summary>
    public Image rarityFrame;

    /// <summary>
    /// 卡牌的描述框
    /// </summary>
    public Image descriptionFrame;

    /// <summary>
    /// 卡面配图
    /// </summary>
    public Image cardImage;

    #region card art

    // [Header("Card Background")]
    // public Sprite AttackBG;
    // public Sprite DefenseBG;
    // public Sprite HealBG;
    // public Sprite SkillBG;
    // public Sprite ExpandBG;
    // public Sprite TrashBG;

    [Header("Card Background")]
    public Sprite BattleFieldBG;
    public Sprite CommandBG;
    public Sprite VirusBG;

    [Header("Card Frame")]
    public Sprite BattleFieldBGF;
    public Sprite CommandBGF;
    public Sprite VirusBGF;

    [Header("Description Frame")]
    public Sprite BattleFieldDescription;
    public Sprite CommandDescription;
    public Sprite VirusDescription;

    [Header("Card Rarity Bar")]
    public Sprite CommonFrame;
    public Sprite UncommonFrame;
    public Sprite RareFrame;
    public Sprite UniqueFrame;
    public Sprite TrashFrame;

    #endregion

    void Start()
    {
        cardBehaviour = GetComponent<CardBehaviour>();
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
    }

    /// <summary>
    /// 设置自定义UI模式
    /// </summary>
    /// <param name="cardMode">卡牌处于牌状态还是砖块状态</param>
    /// <param name="onBeginDrag">开始拖拽时回调</param>
    /// <param name="onDrag">拖拽中回调</param>
    /// <param name="onEndDrag">拖拽结束回调</param>
    /// <param name="onPointerDown">鼠标按下回调</param>
    /// <param name="onPointerUp">鼠标抬起回调</param>
    /// <param name="onPointerEnter">鼠标进入回调</param>
    /// <param name="onPointerExit">鼠标离开回调</param>
    public void SetCustomUIMode(CardMode cardMode, 
        UnityAction onBeginDrag, UnityAction onDrag, UnityAction onEndDrag, 
        UnityAction onPointerDown, UnityAction onPointerUp,
        UnityAction onPointerEnter, UnityAction onPointerExit)
    {
        if (UIState == UIStates.CUSTOM)
        {
            
        }
    }

    /// <summary>
    /// 玩家按下回合结束按钮时自动切换卡牌模式
    /// </summary>
    void OnCardAct()
    {
        // if (UIState == UIStates.PLACED)
        // {
        //     UIState = UIStates.SETTING_TARGET;
        // }
        // else
        // {
        //     UIState = UIStates.ANIMATE;
        // }

        UIState = UIStates.ANIMATE;
    }

    [Header("ColorModifier")]
    [SerializeField] Color CardVirusModifier;

    /// <summary>
    /// 显示卡牌的基础UI信息
    /// </summary>
    void SetCardUI()
    {
        CardData data = cardBehaviour.cardData;

        nameText.text = data.Name;
        descriptionText.text = data.Description;

        nameText.raycastTarget = false;
        descriptionText.raycastTarget = false;

        cardImage.sprite = data.CardTex;

        // switch (data.AbilityType)
        // {
        //     case CardAbilityType.ATTACK:
        //         cardBG.sprite = AttackBG;
        //         break;
        //     case CardAbilityType.DEFEND:
        //         cardBG.sprite = DefenseBG;
        //         break;
        //     case CardAbilityType.HEAL:
        //         cardBG.sprite = HealBG;
        //         break;
        //     case CardAbilityType.SKILL:
        //         cardBG.sprite = SkillBG;
        //         break;
        //     case CardAbilityType.EXPAND:
        //         cardBG.sprite = ExpandBG;
        //         break;
        //     case CardAbilityType.TRASH:
        //         cardBG.sprite = TrashBG;
        //         break;
        // }

        switch (data.ActType)
        {
            case CardActType.BATTLE_FIELD:
                cardBG.sprite = BattleFieldBG;
                cardBGF.sprite = BattleFieldBGF;
                descriptionFrame.sprite = BattleFieldDescription;
                break;
            case CardActType.COMMAND:
                cardBG.sprite = CommandBG;
                cardBGF.sprite = CommandBGF;
                descriptionFrame.sprite = CommandDescription;
                break;
            case CardActType.VIRUS:
                cardBG.sprite = VirusBG;
                cardBGF.sprite = VirusBGF;
                descriptionFrame.sprite = VirusDescription;
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

        if (data.ActType == CardActType.VIRUS)
        {
            cardBG.color = CardVirusModifier;
        }
        else
        {
            cardBG.color = Color.white;
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
