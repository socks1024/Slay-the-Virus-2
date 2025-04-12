using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.ApplyBuffAction(Player, Player, "Tenacity", 1);
    }
}
