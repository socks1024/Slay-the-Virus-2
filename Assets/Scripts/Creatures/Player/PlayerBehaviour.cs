using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CreatureBehaviour<PlayerData>
{
    /// <summary>
    /// 玩家的背包
    /// </summary>
    public PlayerBackPack backPack;

    public override void OnBattleStart()
    {
        BattleManager.Instance.cardFlow.FillDrawPile(backPack.deck.cards);
    }

    public override void OnDead()
    {
        print("PlayerDead");
        EventCenter.Instance.TriggerEvent(EventType.PLAYER_DEAD);
    }
}
