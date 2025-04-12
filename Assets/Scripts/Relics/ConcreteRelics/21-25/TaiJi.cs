using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaiJi : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        ActionLib.ApplyBuffAction(Player, Player, "Counter", 3);
    }
}
