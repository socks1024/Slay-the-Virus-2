using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    /// <summary>
    /// 玩家的背包情况
    /// </summary>
    PlayerBackPack backPack;

    /// <summary>
    /// 遭遇战的数据
    /// </summary>
    EncounterData encounter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
        }
    }

}
