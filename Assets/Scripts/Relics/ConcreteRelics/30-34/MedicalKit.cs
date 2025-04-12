using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.HealAction(Player, Player, 4);
    }
}
