using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimateIntention : MonoBehaviour
{
    /// <summary>
    /// 将会被触发的意图
    /// </summary>
    IntentionBehaviour intention
    { 
        get{ return GetComponent<EnemyBehaviour>().intention;}
        set{ GetComponent<EnemyBehaviour>().intention = value; }
    }

    /// <summary>
    /// 意图所在的位置
    /// </summary>
    public Transform intentionOffset;

    /// <summary>
    /// 将当前的意图设置到给定的位置并显示
    /// </summary>
    public void SetIntentionPosition()
    {
        intention.transform.SetParent(intentionOffset.transform, false);
        intention.transform.localPosition = Vector3.zero;
        intention.gameObject.SetActive(true);
    }

    /// <summary>
    /// 播放为当前意图设置的动画
    /// </summary>
    public void PlayIntentionAnimation()
    {

    }

    /// <summary>
    /// 清除目前显示的所有意图物体
    /// </summary>
    public void ClearIntention()
    {
        Destroy(intention.gameObject);
    }

    
}
