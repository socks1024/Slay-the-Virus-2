using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        base.ActOnCardAct();
        ActionLib.HealAction(Player, Player, DungeonManager.Instance.battleManager.board.AllEmptySquares.Count);
    }
}
