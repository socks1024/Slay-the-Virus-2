using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoZi : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        ActionLib.ApplyBuffAction(Player, Player, "TempStrength", DungeonManager.Instance.battleManager.board.AllDisabledSquares.Count);
    }
}
