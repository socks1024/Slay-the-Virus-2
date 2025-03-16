using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBoard : BoardBehaviour
{
    protected override void InitializeBoard()
    {
        activateSquares = new bool[5,5]
        {
            {true,true,true,true,true},
            {true,true,true,true,true},
            {true,true,true,true,true},
            {true,true,true,true,true},
            {true,true,true,true,true},
        };
    }

    public override void ActOnAllFilledTurnEnd()
    {
        print("BoardAllFilledOnTurnEnd");
    }

    
}
