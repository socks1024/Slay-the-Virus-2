using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntentionShow : MonoBehaviour
{
    /// <summary>
    /// 显示意图强度的文字
    /// </summary>
    TextMeshPro tmp;

    /// <summary>
    /// 显示意图图片
    /// </summary>
    SpriteRenderer sr;

    /// <summary>
    /// 意图数据
    /// </summary>
    IntentionBehaviour intention;

    #region texture

    // public Sprite AttackIntentionSprite;
    // public Sprite DefenseIntentionSprite;
    // public Sprite GainBuffIntentionSprite;
    // public Sprite ApplyBuffIntentionSprite;
    // public Sprite HealIntentionSprite;
    // public Sprite StunIntentionSprite;
    // public Sprite UnknownIntentionSprite;

    #endregion

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        //sr = GetComponent<SpriteRenderer>();
        intention = GetComponent<IntentionBehaviour>();

        tmp.text = intention.Description;

        // switch (intention.intentionType)
        // {
        //     case IntentionType.ATTACK:
        //         sr.sprite = AttackIntentionSprite;
        //         break;
        //     case IntentionType.DEFENSE:
        //         sr.sprite = DefenseIntentionSprite;
        //         break;
        //     case IntentionType.GAIN_BUFF:
        //         sr.sprite = GainBuffIntentionSprite;
        //         break;
        //     case IntentionType.GIVE_DEBUFF:
        //         sr.sprite = ApplyBuffIntentionSprite;
        //         break;
        //     case IntentionType.HEAL:
        //         sr.sprite = HealIntentionSprite;
        //         break;
        //     case IntentionType.STUN:
        //         sr.sprite = StunIntentionSprite;
        //         break;
        //     case IntentionType.UNKNOWN:
        //         sr.sprite = UnknownIntentionSprite;
        //         break;
        // }
    }



    
}
