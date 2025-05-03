using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamage : MonoBehaviour
{
    public CreatureBehaviour Creature{ get{ return GetComponent<CreatureBehaviour>(); } }

    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHealth{ get{ return Creature.MaxHealth; } }

    /// <summary>
    /// 实际生命值
    /// </summary>
    public int Health
    { 
        get { return health; }
        set
        {
            if (value <= 0)
            {
                if (Creature.buffOwner.HasBuff("Phoenix"))
                {
                    value = 1;
                    Creature.buffOwner.GetBuff("Phoenix").Amount -= 1;
                }
                else if (lockHealthAtOne)
                {
                    value = 1;
                }
                else
                {
                    value = 0;
                    ActOnDead?.Invoke();
                }
            }

            if (value > MaxHealth)
            {
                value = MaxHealth;
            }

            health = value;
            
            OnHealthChange?.Invoke(health, block);
        }
    }
    int health;

    public bool lockHealthAtOne = false;

    /// <summary>
    /// 现有的格挡
    /// </summary>
    public int Block
    {
        get { return block; }
        set
        {
            if (value <= 0)
            {
                value = 0;
                if (block > 0) AudioManager.Instance.PlaySFX("DefenseBreak");
            }
            else if (value > 99)
            {
                value = 99;
            }

            block = value;
            
            OnHealthChange?.Invoke(health, block);
        }
    }
    int block;

    /// <summary>
    /// 改变生命状态时触发的回调
    /// </summary>
    public UnityAction<int,int> OnHealthChange;

    /// <summary>
    /// 死亡时触发的回调
    /// </summary>
    public UnityAction ActOnDead;

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">要受到的伤害量</param>
    public void GetDamage(int damage)
    {
        if (damage <= Block)
        {
            Block -= damage;
        }
        else
        {
            damage -= Block;
            Block = 0;
            Health -= damage;
        }
    }

    /// <summary>
    /// 恢复生命
    /// </summary>
    /// <param name="restoration">要恢复的生命量</param>
    public void RestoreHealth(int health)
    {
        Health += health;
    }

    /// <summary>
    /// 获得格挡
    /// </summary>
    /// <param name="block">要获得的格挡量</param>
    public void GainBlock(int block)
    {
        Block += block;
    }

    /// <summary>
    /// 清空格挡
    /// </summary>
    public void RefreshBlock()
    {
        if (!Creature.buffOwner.HasBuff("Fortress"))
        {
            Block = 0;
        }
    }

    void Awake()
    {
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, RefreshBlock);
    }

    public void InitHealth()
    {
        if (Creature is PlayerBehaviour && (Creature as PlayerBehaviour).HasRelic("StrongMedicine"))
        {
            Health = MaxHealth / 2;
        }
        else
        {
            Health = MaxHealth;
        }
        RefreshBlock();
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.ENEMY_ACT_END, RefreshBlock);
    }
}
