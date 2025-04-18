using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class ShakeScreenMovement : MonoBehaviour
{
    public static List<ShakeScreenMovement> shakeScreenMovements= new List<ShakeScreenMovement>();

    public static void ShakeScreen()
    {
        shakeScreenMovements.ForEach(shake => {
            shake.isShaking = true;
            TimersManager.SetTimer("shake", shake.shakeTime, ()=>{
                shake.isShaking = false;
                shake.gameObject.transform.position = shake.originalPosition;
            });
        });
    }

    protected void Awake()
    {
        shakeScreenMovements.Add(this);
        originalPosition = transform.position;
    }

    protected void OnDestroy() 
    {
        shakeScreenMovements.Remove(this);
    }

    public float shakeSpeed = 10f;
    public float shakeTime = 0.5f;
    public float shakeStrength = 10f;
    bool isShaking = false;
    private Vector3 originalPosition;

    void Update()
    {
        if (isShaking)
        {
            float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0);
            float offsetY = Mathf.PerlinNoise(0, Time.time * shakeSpeed);

            Vector3 randomOffset = new Vector3(offsetX, offsetY, 0) * shakeStrength;
            transform.localPosition = originalPosition + randomOffset;
        }
    }
}
