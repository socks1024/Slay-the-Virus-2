using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEffectController : MonoBehaviour
{
    /// <summary>
    /// animation组件
    /// </summary>
    Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        if (!anim.isPlaying)
        {
            Destroy(anim.gameObject);
        }
    }
}
