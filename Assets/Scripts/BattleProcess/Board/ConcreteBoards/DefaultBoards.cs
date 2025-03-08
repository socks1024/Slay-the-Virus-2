using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBoards : BoardBehaviour
{
    public override void ActOnAllFilledTurnEnd()
    {
        print("BoardAllFilledOnTurnEnd");
    }
}
