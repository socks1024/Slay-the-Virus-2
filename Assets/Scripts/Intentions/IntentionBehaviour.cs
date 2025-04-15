using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IntentionShow))]
public class IntentionBehaviour : MonoBehaviour
{
    /// <summary>
    /// 意图的类型
    /// </summary>
    [HideInInspector] public IntentionType IntentionType;

    /// <summary>
    /// 意图的行为
    /// </summary>
    public UnityAction ActOnEnemyTurn;

    /// <summary>
    /// 意图的文字描述
    /// </summary>
    [HideInInspector] public string ShowText;

    public void SetIntention(IntentionType type, string text, UnityAction actOnEnemyTurn)
    {
        this.IntentionType = type;
        this.ShowText = text;
        this.ActOnEnemyTurn = actOnEnemyTurn;

        GetComponent<IntentionShow>().ShowIntention();
    }
}

public enum IntentionType
{
    ATTACK,
    DOUBLE_ATTACK,
    TRIPLE_ATTACK,
    DEFENSE,
    GAIN_BUFF,
    GIVE_DEBUFF,
    ATTACK_AND_GIVE_DEBUFF,
    GAIN_BUFF_AND_GIVE_DEBUFF,
    HEAL,
    STUN,
    SUMMON,
    GIVE_TRASH,
    DEACTIVATE_SQUARE,
    UNKNOWN,
}

public enum TargetType
{
    PLAYER,
    SELF,
    SINGLE_ENEMY,
    ALL_ENEMY,
    RANDOM_ENEMY,
    NO_TARGET,
    MULTIPLE,
}
