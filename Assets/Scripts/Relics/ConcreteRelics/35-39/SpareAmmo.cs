using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpareAmmo : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.ApplyBuffAction(Player, Player, "TempStrength", 3);
    }
}
