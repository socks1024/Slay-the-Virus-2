using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntentionShow : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] TextMeshProUGUI textmesh;

    #region texture

    [Header("意图图片")]
    public Sprite AttackIntentionSprite;
    public Sprite DoubleAttackSprite;
    public Sprite TrippleAttackSprite;
    public Sprite DefenseIntentionSprite;
    public Sprite GainBuffIntentionSprite;
    public Sprite ApplyBuffIntentionSprite;
    public Sprite AttackAndGiveDebuffSprite;
    public Sprite GainBuffAndGiveDebuffSprite;
    public Sprite HealIntentionSprite;
    public Sprite StunIntentionSprite;
    public Sprite UnknownIntentionSprite;

    #endregion

    public void ShowIntention()
    {
        IntentionBehaviour intention = GetComponent<IntentionBehaviour>();

        textmesh.text = intention.ShowText;

        switch (intention.IntentionType)
        {
            case IntentionType.ATTACK:
                image.sprite = AttackIntentionSprite;
                break;
            case IntentionType.DOUBLE_ATTACK:
                image.sprite = DoubleAttackSprite;
                break;
            case IntentionType.TRIPLE_ATTACK:
                image.sprite = TrippleAttackSprite;
                break;
            case IntentionType.DEFENSE:
                image.sprite = DefenseIntentionSprite;
                break;
            case IntentionType.GAIN_BUFF:
                image.sprite = GainBuffIntentionSprite;
                break;
            case IntentionType.GIVE_DEBUFF:
                image.sprite = ApplyBuffIntentionSprite;
                break;
            case IntentionType.ATTACK_AND_GIVE_DEBUFF:
                image.sprite = AttackAndGiveDebuffSprite;
                break;
            case IntentionType.GAIN_BUFF_AND_GIVE_DEBUFF:
                image.sprite = GainBuffAndGiveDebuffSprite;
                break;
            case IntentionType.HEAL:
                image.sprite = HealIntentionSprite;
                break;
            case IntentionType.STUN:
                image.sprite = StunIntentionSprite;
                break;
            case IntentionType.UNKNOWN:
                image.sprite = UnknownIntentionSprite;
                break;
        }
    }



    
}
