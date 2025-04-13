using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranium : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        if (DungeonManager.Instance.battleManager.turnCount == 6)
        {
            ActionLib.DamageAllAction(Player, 30);
        }
    }
}
