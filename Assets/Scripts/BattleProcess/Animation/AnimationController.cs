using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoSingleton<AnimationController>
{
    /// <summary>
    /// 要播放的动画的动画队列
    /// </summary>
    public Queue<Animation> animationQueue = new Queue<Animation>();
    

    /// <summary>
    /// 当所有卡牌操作动画结束时调用
    /// </summary>
    public void OnCardActAnimationEnd()
    {
        //EventCenter.Instance.TriggerEvent(EventType.CARD_ACT_END);
    }

    

    /// <summary>
    /// 当所有敌人行动动画结束时调用
    /// </summary>
    public void OnEnemyActAnimationEnd()
    {
        //EventCenter.Instance.TriggerEvent(EventType.ENEMY_ACT_END);
    }
}
