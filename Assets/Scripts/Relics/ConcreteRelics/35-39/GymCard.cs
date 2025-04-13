using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymCard : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.ApplyBuffAction(Player, Player, "Strength", 1);
    }
}
