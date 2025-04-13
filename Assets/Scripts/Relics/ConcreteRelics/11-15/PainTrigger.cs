using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainTrigger : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        if (Player.takeDamage.Health <= Player.MaxHealth/2) ActionLib.ApplyBuffAction(Player, Player, "TempStrength", 2);
    }
}
