using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    public Board board;
    public CardFlowController cardFlow;
    public EnemyGroup enemyGroup;
}
