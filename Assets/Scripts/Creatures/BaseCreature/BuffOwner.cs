using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuffOwner : MonoBehaviour
{
    /// <summary>
    /// 持有的各种状态效果
    /// </summary>
    List<BuffBehaviour> buffs = new List<BuffBehaviour>();

    /// <summary>
    /// 改变Buff时触发的回调
    /// </summary>
    public UnityAction<List<BuffBehaviour>> OnChangeBuff;

    /// <summary>
    /// 获得一些Buff
    /// </summary>
    /// <param name="newBuff">新的Buff</param>
    public void GainBuff(BuffBehaviour newBuff)
    {
        bool hasSameBuff = false;

        foreach (BuffBehaviour oldBuff in buffs)
        {
            if (oldBuff.ID == newBuff.ID)
            {
                oldBuff.Amount += newBuff.Amount;
                hasSameBuff = true;
            }
        }

        if (!hasSameBuff)
        {
            buffs.Add(newBuff);
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
    /// 清除所有Buff
    /// </summary>
    public void ClearBuff()
    {
        buffs.Clear();
    }

    void Start()
    {
        ClearBuff();
        EventCenter.Instance.AddEventListener(EventType.ACT_START, ClearBuff);
    }


}