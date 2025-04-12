using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHorn : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.ApplyBuffAction(Player, Player, "Counter", 3);
    }
}
