using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoSingleton<AnimationManager>
{
    /// <summary>
    /// 动画特效列表
    /// </summary>
    public List<Animation> animEffects = new List<Animation>();

    /// <summary>
    /// 在特定的位置播放特定的动画特效
    /// </summary>
    /// <param name="position">动画位置</param>
    /// <param name="name">动画名称</param>
    public void PlayAnimEffect(Vector3 position, string name)
    {
        
    }
}
