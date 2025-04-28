using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText;

    [SerializeField] Image image;

    RewardType rewardType;

    public RewardItem ShowReward(int amount, RewardType rewardType)
    {
        amountText.text = amount.ToString();

        this.rewardType = rewardType;

        return this;
    }

    public void ShowCard(string cardID)
    {
        if (rewardType == RewardType.CARD)
        {
            CardBehaviour card = Instantiate(CardLib.GetCard(cardID), transform);
            card.transform.SetAsFirstSibling();
            image.gameObject.SetActive(false);
            card.GetComponent<CardUI>().UIState = UIStates.ANIMATE;
        }
    }

    public void ShowRelic(string relicID)
    {
        if (rewardType == RewardType.RELIC)
        {
            
        }
    }
}

public enum RewardType
{
    MONEY,
    CARD,
    RELIC,
}
