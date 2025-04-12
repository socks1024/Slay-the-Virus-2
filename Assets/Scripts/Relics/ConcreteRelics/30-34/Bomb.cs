using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.DamageAllAction(Player, 6);
    }
}
