using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;

public class AnimationManager : MonoSingletonDestroyOnLoad<AnimationManager>
{
    [SerializeField] AnimationPlayMode PlayMode;

    /// <summary>
    /// 动画特效列表
    /// </summary>
    public SerializableDictionary<AnimEffectType, SequenceFrame> animations;

    /// <summary>
    /// 动画特效列表
    /// </summary>
    public SerializableDictionary<AnimEffectType, ParticleSystem> particles;

    /// <summary>
    /// 在特定的位置播放特定的动画特效
    /// </summary>
    /// <param name="position">动画位置</param>
    /// <param name="name">动画名称</param>
    /// <param name="onComplete">动画结束时回调</param>
    /// <param name="animationPlayMode">动画播放方式</param>
    public void PlayAnimEffect(Vector3 position, AnimEffectType name, UnityAction onComplete, AnimationPlayMode animationPlayMode)
    {
        if (animationPlayMode == AnimationPlayMode.DISABLED || name == AnimEffectType.NONE)
        {
            onComplete.Invoke();
        }
        else if (animationPlayMode == AnimationPlayMode.SIMPLE)
        {
            position.z -= 1;
            AnimEffectController anim = Instantiate(animations[name], position, Quaternion.identity).GetComponent<AnimEffectController>();
            anim.onComplete += onComplete;
            anim.PlayAnimation();
        }
        else if (animationPlayMode == AnimationPlayMode.PARTICLE)
        {
            position.z -= 1;
            AnimEffectController anim = Instantiate(particles[name], position, Quaternion.identity).GetComponent<AnimEffectController>();
            anim.onComplete += onComplete;
            anim.PlayParticleEffect();
        }
    }

    /// <summary>
    /// 在特定的位置播放特定的动画特效
    /// </summary>
    /// <param name="position">动画位置</param>
    /// <param name="name">动画名称</param>
    /// <param name="onComplete">动画结束时回调</param>
    public void PlayAnimEffect(Vector3 position, AnimEffectType name, UnityAction onComplete)
    {
        PlayAnimEffect(position, name, onComplete, PlayMode);
    }

    
}

public enum AnimationPlayMode
{
    SIMPLE,
    DISABLED,
    PARTICLE,
}

public enum AnimEffectType
{
    NONE,
    DAMAGED,
    POSITIVE_BUFF,
    NEGATIVE_BUFF,
    HEALED,
    WOUND,
    COUNTER,
}
