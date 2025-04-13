using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : RelicBehaviour
{
    public override void ActOnBattleStart()
    {
        ActionLib.ApplyBuffAction(Player, Player, "Tenacity", 1);
    }
}
