using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsHolder : MonoBehaviour
{
    public Reward rewardData;

    PlayerBehaviour Player{ get{ return DungeonManager.Instance.Player; }}

    public int cardAmount = 4;

    public int relicAmount = 0;

    public void GenerateRewards(LootInfo lootInfo)
    {
        rewardData = new();

        print("???");

        ClearRewardsPanel();
        
        rewardData.money = lootInfo.RandomGetMoney();

        rewardData.cardRewardInfos = new();
        lootInfo.RandomGetCards(cardAmount).ForEach(cardRewardInfo => rewardData.cardRewardInfos.Add(cardRewardInfo));
        
        rewardData.relicRewardInfos = new();
        lootInfo.RandomGetRelics(relicAmount).ForEach(relicRewardInfo => rewardData.relicRewardInfos.Add(relicRewardInfo));

        ShowRewards();

        GetRewards();
    }

    public void GetRewards()
    {
        Player.GetRewards(rewardData);
    }


    #region Reward UI
    
    /// <summary>
    /// 现在显示的奖励
    /// </summary>
    List<GameObject> rewards = new List<GameObject>();

    [SerializeField] RewardItem RewardItemPrefab;

    public GameObject content;

    void ShowRewards()
    {
        Instantiate(RewardItemPrefab, content.transform)
            .ShowReward(rewardData.money, RewardType.MONEY);

        foreach (CardRewardInfo cardRewardInfo in rewardData.cardRewardInfos)
        {
            Instantiate(RewardItemPrefab, content.transform)
                .ShowReward(cardRewardInfo.amount, RewardType.CARD)
                .ShowCard(cardRewardInfo.cardID);
        }

        foreach (RelicRewardInfo relicRewardInfo in rewardData.relicRewardInfos)
        {
            Instantiate(RewardItemPrefab, content.transform)
                .ShowReward(relicRewardInfo.amount, RewardType.RELIC)
                .ShowRelic(relicRewardInfo.relicID);
        }
    }

    public void ClearRewardsPanel()
    {
        for (int i = rewards.Count - 1; i >= 0; i--)
        {
            Destroy(rewards[i]);
        }
    }

    #endregion
}

public struct Reward
{
    public int money;

    public List<CardRewardInfo> cardRewardInfos;

    public List<RelicRewardInfo> relicRewardInfos;
}
