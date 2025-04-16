using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IntentionShow))]
public class IntentionBehaviour : MonoBehaviour
{
    /// <summary>
    /// 意图的类型
    /// </summary>
    [HideInInspector] public IntentionType IntentionType;

    public UnityAction ActOnEnemyTurn;

    public UnityAction ActBeforeCardAct;

    /// <summary>
    /// 意图的文字描述
    /// </summary>
    [HideInInspector] public string ShowText;

    public void SetIntention(IntentionType type, string text, UnityAction actOnEnemyTurn, UnityAction actBeforeCardAct)
    {
        this.IntentionType = type;
        this.ShowText = text;
        this.ActOnEnemyTurn = actOnEnemyTurn;
        this.ActBeforeCardAct = actBeforeCardAct;

        GetComponent<IntentionShow>().ShowIntention();
    }

    public void SetIntention(IntentionInfo info)
    {
        this.IntentionType = info.type;
        this.ShowText = info.text;
        this.ActOnEnemyTurn = info.actOnEnemyTurn;
        this.ActBeforeCardAct = info.actBeforeCardAct;

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

public struct IntentionInfo
{
    public IntentionType type;
    public string text;
    public UnityAction actOnEnemyTurn;
    public UnityAction actBeforeCardAct;

    public IntentionInfo(IntentionType type, string text, UnityAction actOnEnemyTurn, UnityAction actBeforeCardAct)
    {
        this.type = type;
        this.text = text;
        this.actOnEnemyTurn = actOnEnemyTurn;
        this.actBeforeCardAct = actBeforeCardAct;
    }
}