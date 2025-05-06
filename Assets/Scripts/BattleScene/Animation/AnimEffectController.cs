using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimEffectController : MonoBehaviour
{
    public UnityAction onComplete;

    public void PlayAnimation()
    {
        GetComponent<SequenceFrame>().PlayAnimation();
        Shake();
        PlaySound();
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
        PlaySound();
        onComplete.Invoke();
    }

    #region Camera Impulse

    [Header("相机震动")]
    [SerializeField] bool UseShake;
    [SerializeField] float ShakeForce;

    void Shake()
    {
        if (UseShake)
        {
            ShakeScreenMovement.ShakeScreen();
        }
    }

    #endregion

    #region Audio Effect

    [Header("音效")]
    [SerializeField] string SoundID;

    void PlaySound()
    {
        AudioManager.Instance.PlaySFX(SoundID);
    }

    #endregion
}
