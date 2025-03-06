using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyBehaviour
{
    public override void SetIntention(int turnCount)
    {
        if (turnCount % 2 == 1)
        {
            GetComponent<HoldIntention>().SetIntention("DefaultIntention1");
        }
        else
        {
            GetComponent<HoldIntention>().SetIntention("DefaultIntention2");
        }
    }
}
