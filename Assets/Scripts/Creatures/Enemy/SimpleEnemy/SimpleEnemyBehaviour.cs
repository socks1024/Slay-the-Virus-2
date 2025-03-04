using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HoldIntention))]
[RequireComponent(typeof(AnimateIntention))]
public abstract class SimpleEnemyBehaviour : CreatureBehaviour<SimpleEnemyData>
{
    /// <summary>
    /// 可以触发的所有意图
    /// </summary>
    public List<IntentionBehaviour> IntentionsAvailable;
    

    
}
