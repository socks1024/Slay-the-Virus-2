using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal : RelicBehaviour
{
    public override void ActOnCardAct()
    {
        if (BoardAllFilled) ActionLib.PlayerChangeMoney(15);
    }
}
