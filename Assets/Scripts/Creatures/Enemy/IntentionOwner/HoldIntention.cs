using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldIntention : MonoBehaviour
{
    /// <summary>
    /// 将会依次被触发的意图队列
    /// </summary>
    List<IntentionBehaviour> intentionsQueue = new List<IntentionBehaviour>();

    /// <summary>
    /// 若一个特定的意图可用，将其加入意图队列
    /// </summary>
    /// <param name="ID">该意图的ID</param>
    public void AddIntention(string ID)
    {
        foreach (IntentionBehaviour intention in GetComponent<SimpleEnemyBehaviour>().IntentionsAvailable)
        {
            if(ID == intention.ID)
            {
                intentionsQueue.Add(intention);
            }
        }
    }
}
