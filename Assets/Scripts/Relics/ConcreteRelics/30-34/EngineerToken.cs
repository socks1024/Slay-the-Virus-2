using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerToken : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.GainBlockAction(Player, Player, 8);
    }
}
