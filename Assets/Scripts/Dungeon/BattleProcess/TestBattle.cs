using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    /// <summary>
    /// 卡组
    /// </summary>
    public List<CardBehaviour> cardPrefabs;

    /// <summary>
    /// 棋盘
    /// </summary>
    public BoardBehaviour boardPrefab;

    /// <summary>
    /// 所有敌人
    /// </summary>
    public List<EnemyBehaviour> enemyPrefabs;

    bool b = true;

    public Vector3 testPos = Vector3.zero;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && b)
        {
            // DungeonManager.Instance.battleManager.player.SetBackpack(InstantiateHelper.MultipleInstatiate<CardBehaviour>(cardPrefabs), Instantiate(boardPrefab), 0);
            // DungeonManager.Instance.battleManager.InitializeEncounter(InstantiateHelper.MultipleInstatiate<EnemyBehaviour>(enemyPrefabs));
            // EventCenter.Instance.TriggerEvent(EventType.BATTLE_START);
            b = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AnimationManager.Instance.PlayAnimEffect(testPos, "beat");
        }
    }

}
