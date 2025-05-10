using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuffOwner : MonoBehaviour
{
    public CreatureBehaviour Creature{ get{ return GetComponent<CreatureBehaviour>(); }}

    /// <summary>
    /// 持有的各种状态效果
    /// </summary>
    List<BuffBehaviour> buffs = new List<BuffBehaviour>();

    /// <summary>
    /// 改变Buff时触发的回调
    /// </summary>
    public UnityAction<List<BuffBehaviour>> OnChangeBuff;

    /// <summary>
    /// 获得buff
    /// </summary>
    /// <param name="buffName">buff的ID</param>
    /// <param name="amount">buff的量</param>
    public void GainBuff(string buffName, int amount)
    {

        if (HasBuff(buffName))
        {
            GetBuff(buffName).Amount += amount;
        }
        else
        {
            buffs.Add(DungeonBuffLib.GetBuff(buffName, amount, Creature));
        }

        OnChangeBuff.Invoke(buffs);
    }

    public void GainBuffNextTurn(string buffName, int amount, CreatureBehaviour source)
    {
        foreach (BuffBehaviour buff in buffs)
        {
            if (buff.ID == "ApplyBuffNextTurn" && !buff.isDestroying)
            {
                ApplyBuffNextTurnBuff applyBuffNextTurnBuff = buff as ApplyBuffNextTurnBuff;

                if (applyBuffNextTurnBuff.newBuffID == buffName && applyBuffNextTurnBuff.source == source)
                {
                    applyBuffNextTurnBuff.Amount += amount;
                    return;
                }
            }
        }

        buffs.Add(DungeonBuffLib.GetApplyBuffNextTurnBuff(buffName, amount, Creature, source));

        OnChangeBuff.Invoke(buffs);
    }

    /// <summary>
    /// 获取对象是否持有特定Buff
    /// </summary>
    /// <param name="ID">buffID</param>
    /// <returns>是否持有</returns>
    public bool HasBuff(string ID)
    {
        foreach (BuffBehaviour oldBuff in buffs)
        {
            if (oldBuff.ID == ID)
            {
                if (!oldBuff.isDestroying)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 获取对象的特定buff
    /// </summary>
    /// <param name="ID">buff的ID</param>
    /// <returns>特定buff</returns>
    public BuffBehaviour GetBuff(string ID)
    {
        foreach (BuffBehaviour oldBuff in buffs)
        {
            if (oldBuff.ID == ID)
            {
                return oldBuff;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取特定Buff的持有量
    /// </summary>
    /// <param name="ID">特定Buff的ID</param>
    /// <returns>特定Buff的持有量</returns>
    public int GetBuffAmount(string ID)
    {
        foreach (BuffBehaviour oldBuff in buffs)
        {
            if (oldBuff.ID == ID)
            {
                return oldBuff.Amount;
            }
        }
        return 0;
    }

    /// <summary>
    /// 获取最高的负面buff
    /// </summary>
    /// <returns></returns>
    public BuffBehaviour GetHighestDebuff()
    {
        int amount = 0;

        BuffBehaviour retBuff = null;

        foreach (BuffBehaviour buff in buffs)
        {
            if (buff.Amount > amount && buff.Type == BuffType.NEGATIVE)
            {
                amount = buff.Amount;
                retBuff = buff;
            }
        }

        return retBuff;
    }

    public void RemoveBuff(BuffBehaviour buff)
    {
        if (buffs.Contains(buff)) buffs.Remove(buff);
    }

    /// <summary>
    /// 清除所有Buff
    /// </summary>
    public void ClearBuff()
    {
        buffs.Clear();
    }

    void Awake()
    {
        ClearBuff();
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, ClearBuff);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_START, ClearBuff);
    }


}