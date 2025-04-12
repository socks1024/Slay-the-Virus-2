using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        ActionLib.ApplyBuffAction(DungeonManager.Instance.battleManager.enemyGroup.GetRandomEnemy(), Player, "Stun", 1); 
    }
}
