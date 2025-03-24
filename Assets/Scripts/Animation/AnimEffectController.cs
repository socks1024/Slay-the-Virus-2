using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEffectController : MonoBehaviour
{
    public void Play()
    {
        GetComponent<SequenceFrame>().PlayAnimation();
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
