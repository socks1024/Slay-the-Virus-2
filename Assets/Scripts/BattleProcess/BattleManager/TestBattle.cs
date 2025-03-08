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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BattleManager.Instance.player.SetBackpack(InstantiateHelper.MultipleInstatiate<CardBehaviour>(cardPrefabs), Instantiate(boardPrefab), 0);
            BattleManager.Instance.InitializeEncounter(InstantiateHelper.MultipleInstatiate<EnemyBehaviour>(enemyPrefabs));
        }
    }

}
