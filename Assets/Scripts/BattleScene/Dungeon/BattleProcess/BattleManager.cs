using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Timers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [HideInInspector] public DungeonNode battleNode;

    [HideInInspector] public BoardBehaviour board;

    [Header("战斗时元素/UI")]
    public Transform boardRoot;

    public Transform relicsRoot;

    public CardFlowController cardFlow;

    public EnemyGroup enemyGroup;

    public PlayerBehaviour player;

    [HideInInspector] public int turnCount = 1;

    #region battle start phase

    /// <summary>
    /// 初始化遭遇战并开始战斗
    /// </summary>
    public void InitializeEncounter(DungeonNode battleInfo)
    {
        this.battleNode = battleInfo;

        board = Instantiate(player.p_Board, boardRoot);

        List<CardBehaviour> deck = InstantiateHelper.MultipleInstatiate(player.p_Deck);
        cardFlow.FillDrawPile(deck);

        List<RelicBehaviour> relics = InstantiateHelper.MultipleInstatiate(player.p_Relics, relicsRoot);

        List<GameObject> enemies = InstantiateHelper.MultipleInstatiate((battleInfo.nodeInfo as BattleNodeInfo).p_Enemies_GameObject);
        enemies.ForEach(e => {enemyGroup.AddEnemyToBattle(e.GetComponent<EnemyBehaviour>());});

        if (battleInfo.nodeInfo is EvacuateBattleNodeInfo)
        {
            moreEnemies = (battleInfo.nodeInfo as EvacuateBattleNodeInfo).p_AfterEnemies;
        }
    }

    /// <summary>
    /// 战斗开始时触发的回调
    /// </summary>
    void OnBattleStart()
    {
        EventCenter.Instance.TriggerEvent(EventType.TURN_START);
    }

    #endregion

    #region turn logic phase

    // BATTLE_START:
    // TURN_START:
    // ACT_START:按下按钮触发
    // CARD_ACT_END:弃牌动画播放完毕 && 所有卡牌动画播放完毕
    // ENEMY_ACT_END:所有敌人动画播放完毕

    void OnTurnStart()
    {
        AudioManager.Instance.PlaySFX("PlayerTurn");
    }

    /// <summary>
    /// 触发卡牌按钮操作
    /// </summary>
    public void TriggerCardActRelics()
    {
        relicsRoot.GetComponentsInChildren<RelicBehaviour>().ToList().ForEach(r => r.ActOnCardAct());
    }

    /// <summary>
    /// 弃牌动画是否已经结束
    /// </summary>
    public bool DiscardAnimFinished
    {
        get{ return discardAnimFinished; }
        set
        {
            discardAnimFinished = value;
            if (discardAnimFinished == true) print("discard finished");
            if (discardAnimFinished && playAnimFinished)
            {
                TriggerCardActEnd();
            }
        }
    }
    bool discardAnimFinished;

    /// <summary>
    /// 出牌动画是否已经结束
    /// </summary>
    public bool PlayAnimFinished
    {
        get{ return playAnimFinished; }
        set
        {
            playAnimFinished = value;
            if (playAnimFinished == true) print("play finished");
            if (discardAnimFinished && playAnimFinished)
            {
                TriggerCardActEnd();
            }
        }
    }
    bool playAnimFinished;

    [SerializeField] float CardEnemyActInterval = 0.5f;

    /// <summary>
    /// 当 弃牌动画播放完毕 & 所有卡牌动画播放完毕 时，触发卡牌行动结束
    /// </summary>
    void TriggerCardActEnd()
    {
        TimersManager.SetTimer("CardEnemyActInterval", CardEnemyActInterval, () =>{
            EventCenter.Instance.TriggerEvent(EventType.CARD_ACT_END);
            discardAnimFinished = false;
            playAnimFinished = false;
        });
    }

    [SerializeField] float NextTurnInterval = 0.5f;

    bool battleEnded = false;

    /// <summary>
    /// 在所有行动结束时触发
    /// </summary>
    void OnAllActEnd()
    {
        TimersManager.SetTimer("Wait", 0.5f, () =>{
            BuffBehaviour.TriggerAllActOnTurnEnd();
            TimersManager.SetTimer("NextTurnInterval", NextTurnInterval, ()=>{
                turnCount += 1;
                if (!battleEnded) EventCenter.Instance.TriggerEvent(EventType.TURN_START);
            });
        });
    }

    /// <summary>
    /// 当所有敌人都被击杀时触发
    /// </summary>
    public void OnAllEnemyDestroyed()
    {
        EventCenter.Instance.TriggerEvent(EventType.BATTLE_WIN);

        AudioManager.Instance.StopMusic();
    }

    #endregion

    #region endurance battle

    /// <summary>
    /// 当前战斗是否为耐久战斗
    /// </summary>
    bool IsEnduranceBattle{ get{ return battleNode.nodeInfo is EvacuateBattleNodeInfo;}}

    /// <summary>
    /// 战斗的耐久回合数
    /// </summary>
    int enduranceTurn{ get{ return (battleNode.nodeInfo as EvacuateBattleNodeInfo).enduranceTurn;}}

    /// <summary>
    /// 当前耐久战斗进展到的波数
    /// </summary>
    int currentWave = 0;

    /// <summary>
    /// 接下来的数波敌人
    /// </summary>
    List<List<EnemyBehaviour>> moreEnemies;

    #endregion

    #region battle end

    [Header("战利品UI")]
    [SerializeField] RewardsHolder rewardPanel;

    [SerializeField] float winLeaveDelay = 1f;

    /// <summary>
    /// 战斗胜利时触发的回调
    /// </summary>
    void OnBattleWin()
    {
        // ResetBattleElements();
        battleEnded = true;
        ShowRewards();
        AudioManager.Instance.PlaySFX("Win");
    }

    /// <summary>
    /// 展示战利品
    /// </summary>
    void ShowRewards()
    {
        rewardPanel.gameObject.SetActive(true);
        rewardPanel.GenerateRewards((battleNode.nodeInfo as BattleNodeInfo).lootInfo);
        //显示战利品
    }

    

    /// <summary>
    /// 获得战利品后触发的回调
    /// </summary>
    public void OnGetRewards()
    {
        rewardPanel.gameObject.SetActive(false);

        // DungeonManager.Instance.RightBGReturnBack(() => {
        //     DungeonManager.Instance.EnterNode(battleNode.connectedNodes[0]);
        // });

        WinLeaveDungeon();
    }

    void WinLeaveDungeon()
    {
        TimersManager.SetTimer("WinLeaveDungeon", winLeaveDelay, DungeonManager.Instance.WinLeaveDungeon);
    }

    /// <summary>
    /// 战斗失败时触发的回调
    /// </summary>
    void OnPlayerDead()
    {
        battleEnded = true;
        AudioManager.Instance.PlaySFX("Lose");
        TimersManager.SetTimer("LoseMusic", 1, ()=>AudioManager.Instance.PlaySFX("LoseMusic"));
        DungeonManager.Instance.LoseLeaveDungeon();
    }

    // /// <summary>
    // /// 战斗结束后的清理
    // /// </summary>
    // void ResetBattleElements()
    // {
    //     //板子淡出
    //     Destroy(board.gameObject);
    //     board = null;

    //     cardFlow.hand.ClearCards();
    //     cardFlow.drawPile.ClearCards();
    //     cardFlow.discardPile.ClearCards();

    //     enemyGroup.ResetEnemyGroup();

    //     relicsRoot.GetComponentsInChildren<RelicBehaviour>().ToList().ForEach(r => Destroy(r.gameObject));

    //     currentWave = 0;
    // }

    #endregion

    void Awake()
    {
        EventCenter.Instance.AddEventListener(EventType.BATTLE_START, OnBattleStart);
        EventCenter.Instance.AddEventListener(EventType.TURN_START, OnTurnStart);
        EventCenter.Instance.AddEventListener(EventType.ENEMY_ACT_END, OnAllActEnd);
        EventCenter.Instance.AddEventListener(EventType.BATTLE_WIN, OnBattleWin);
        EventCenter.Instance.AddEventListener(EventType.PLAYER_DEAD, OnPlayerDead);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_START, OnBattleStart);
        EventCenter.Instance.RemoveEventListener(EventType.TURN_START, OnTurnStart);
        EventCenter.Instance.RemoveEventListener(EventType.ENEMY_ACT_END, OnAllActEnd);
        EventCenter.Instance.RemoveEventListener(EventType.BATTLE_WIN, OnBattleWin);
        EventCenter.Instance.RemoveEventListener(EventType.PLAYER_DEAD, OnPlayerDead);
    }
}
