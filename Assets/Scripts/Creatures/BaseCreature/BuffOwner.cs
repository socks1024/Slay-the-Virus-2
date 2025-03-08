using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffOwner : MonoBehaviour
{
    /// <summary>
    /// 持有的各种状态效果
    /// </summary>
    List<BuffBehaviour> Buffs{ get; set;}

    /// <summary>
    /// 获得一些Buff
    /// </summary>
    /// <param name="newBuff">新的Buff</param>
    public void GainBuff(BuffBehaviour newBuff)
    {
        bool hasSameBuff = false;

        foreach (BuffBehaviour oldBuff in Buffs)
        {
            if (oldBuff.ID == newBuff.ID)
            {
                oldBuff.Amount += newBuff.Amount;
                hasSameBuff = true;
            }
        }

        if (!hasSameBuff)
        {
            Buffs.Add(newBuff);
            newBuff.Owner = GetComponent<CreatureBehaviour>();
        }
    }

    /// <summary>
    /// 获取特定Buff的持有量
    /// </summary>
    /// <param name="ID">特定Buff的ID</param>
    /// <returns>特定Buff的持有量</returns>
    public int GetBuffAmount(string ID)
    {
        foreach (BuffBehaviour oldBuff in Buffs)
        {
            if (oldBuff.ID == ID)
            {
                return oldBuff.Amount;
            }
        }
        return 0;
    }

    /// <summary>
    /// 清除所有Buff
    /// </summary>
    public void ClearBuff()
    {
        Buffs.Clear();
    }

    void Start()
    {
        ClearBuff();
        EventCenter.Instance.AddEventListener(EventType.TURN_END, ClearBuff);
    }
}