using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Timers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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



    #region Flash

    [Header("闪烁")]
    public Color FlashColor;
    public float FlashDuration = 0;

    public void StartFlash(Image image)
    {

        StartCoroutine("Flash", image);
    }

    IEnumerator Flash(Image image)
    {
        float time = 0;

        while (time <= FlashDuration)
        {
            if (image != null) image.color = Color.Lerp(FlashColor, Color.white, time / FlashDuration);

            time += Time.deltaTime;

            yield return null;
        }
        image.color = Color.white;
    }

    #endregion

    #region Number

    [Header("数字")]

    public GameObject root;

    public TextMeshProUGUI numberPrefab;

    public float maxFadeDuration;

    public float minFadeDuration;

    public float offsetDistance;

    public float maxSize;

    public int numberAtMaxSize;

    public float minSize;

    public SerializableDictionary<NumberType, Color> typeColors = new();

    public void ShowNumber(int number, NumberType type, Vector3 position)
    {
        print("show number");
        TextMeshProUGUI numText = Instantiate(numberPrefab, root.transform);
        numText.text = number.ToString();
        numText.transform.position = position + new Vector3(Random.value, Random.value, 0).normalized * (offsetDistance * Random.value);
        numText.transform.localScale *= Mathf.Lerp(minSize, maxSize, number / numberAtMaxSize);
        numText.color = typeColors[type];

        StartCoroutine(NumberFade(numText.gameObject));
    }

    IEnumerator NumberFade(GameObject numberObj)
    {
        float time = 0;
        float duration = Random.Range(minFadeDuration, maxFadeDuration);
        CanvasGroup canvasGroup = numberObj.GetComponent<CanvasGroup>();

        while (time <= duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time/duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(numberObj);
    }

    #endregion
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

public enum NumberType
{
    DAMAGE,
    HEAL,
    GAIN_BLOCK,
    WOUND,
}