using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SequenceFrame : MonoBehaviour
{
    /// <summary>
    /// 所有的序列帧
    /// </summary>
    public List<Sprite> sprites;

    /// <summary>
    /// sprite
    /// </summary>
    SpriteRenderer sr;

    /// <summary>
    /// 目前运行了几秒
    /// </summary>
    float seconds = 0;

    /// <summary>
    /// 是否正在播放动画
    /// </summary>
    bool isPlaying = false;

    /// <summary>
    /// 每秒播放多少帧
    /// </summary>
    public int framePerSecond = 12;

    void Start() 
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPlaying)
        {
            seconds += Time.deltaTime;
            int i = (int)(seconds * framePerSecond);
            
            if (i >= sprites.Count)
            {
                isPlaying = false;
                seconds = 0;
                sr.sprite = sprites[0];
                OnAnimationEnd.Invoke();
            }
            else
            {
                sr.sprite = sprites[i];
            }
        }
    }
    //设每秒播放60帧
    //每秒播放n幅画面：
    //每幅画面实际占用1/n秒 = 1/n * 60帧 = n / 60 帧
    //每 n/60 = x 帧切换一幅画面
    //切换到第 frameCount / x 帧画面

    /// <summary>
    /// 播放序列帧
    /// </summary>
    public void PlayAnimation()
    {
        isPlaying = true;
    }

    /// <summary>
    /// 结束时触发的事件
    /// </summary>
    public UnityEvent OnAnimationEnd;
}
