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
    public Sprite DefenseIntentionSprite;
    public Sprite GainBuffIntentionSprite;
    public Sprite ApplyBuffIntentionSprite;
    public Sprite HealIntentionSprite;
    public Sprite StunIntentionSprite;
    public Sprite UnknownIntentionSprite;

    #endregion

    void Awake()
    {
        print("intention start");
        
        IntentionBehaviour intention = GetComponent<IntentionBehaviour>();

        textmesh.text = intention.Amount.ToString();

        switch (intention.IntentionType)
        {
            case IntentionType.ATTACK:
                image.sprite = AttackIntentionSprite;
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
