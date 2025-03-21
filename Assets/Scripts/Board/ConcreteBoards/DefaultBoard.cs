using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBoard : BoardBehaviour
{
    protected override void InitializeBoard()
    {
        activateSquares = new bool[5,5]
        {
            {false,false,false,false,false},
            {true,true,true,true,true},
            {true,true,true,true,true},
            {true,true,true,true,true},
            {false,false,false,false,false},
        };
    }

    public override void ActOnAllFilledTurnEnd()
    {
        print("BoardAllFilledOnTurnEnd");
    }

    
}
