using UnityEngine;

[RequireComponent(typeof(IntentionShow))]
public abstract class IntentionBehaviour : MonoBehaviour
{
    /// <summary>
    /// 意图的ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 意图的类型
    /// </summary>
    public IntentionType IntentionType;

    /// <summary>
    /// 意图目标的类型
    /// </summary>
    public TargetType TargetType;

    /// <summary>
    /// 意图的强度
    /// </summary>
    [HideInInspector]public int Amount;

    /// <summary>
    /// 意图的强度
    /// </summary>
    [HideInInspector]public int Amount2;

    /// <summary>
    /// 意图的目标
    /// </summary>
    [HideInInspector]public CreatureBehaviour target;
    
    /// <summary>
    /// 意图的来源
    /// </summary>
    [HideInInspector]public CreatureBehaviour source;

    /// <summary>
    /// 意图的行为
    /// </summary>
    public abstract void ActOnEnemyTurn();

    protected virtual void Awake()
    {
        
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
    NO_TARGET,
}
