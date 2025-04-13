using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPressureMonitor : RelicBehaviour
{
    public override void ActOnTurnStart()
    {
        base.ActOnTurnStart();
        if (Player.takeDamage.Health >= Player.MaxHealth) ActionLib.ApplyBuffAction(Player, Player, "Strength", 1);
    }
}
