using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class AnimEffectController : MonoBehaviour
{
    public UnityAction onComplete;

    public void PlayAnimation()
    {
        GetComponent<SequenceFrame>().PlayAnimation();
        Shake();
    }

    public void OnAnimationEnd()
    {
        onComplete.Invoke();
        Destroy(gameObject);
    }

    public void PlayParticleEffect()
    {
        GetComponent<ParticleSystem>().Play();
        Shake();
        onComplete.Invoke();
    }

    #region Camera Impulse

    [Header("相机震动")]
    [SerializeField] bool UseShake;
    [SerializeField] float ShakeForce;

    public void Shake()
    {
        if (UseShake)
        {
            ShakeScreenMovement.ShakeScreen();
        }
    }

    #endregion
}
