using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsHolder : MonoBehaviour
{
    /// <summary>
    /// 现在显示的奖励图片
    /// </summary>
    List<GameObject> rewardsImg = new List<GameObject>();

    /// <summary>
    /// 展示战利品
    /// </summary>
    /// <param name="lootInfo">战利品信息</param>
    public void ShowRewards(LootInfo lootInfo)
    {
        print(lootInfo);
    }

    /// <summary>
    /// 清空战利品界面
    /// </summary>
    public void ClearRewards()
    {
        for (int i = rewardsImg.Count - 1; i >= 0; i--)
        {
            Destroy(rewardsImg[i]);
        }
    }
}
