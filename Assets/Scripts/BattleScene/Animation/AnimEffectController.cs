using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEffectController : MonoBehaviour
{
    public void Play()
    {
        GetComponent<SequenceFrame>().PlayAnimation();
    }

    public UnityAction onComplete;

    public void OnAnimationEnd()
    {
        onComplete.Invoke();
        Destroy(gameObject);
    }
}
