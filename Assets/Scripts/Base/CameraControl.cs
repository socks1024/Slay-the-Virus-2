using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    public enum FocusState { Idle, Focusing, Waiting, Returning }

    [Header("聚焦配置")]
    [SerializeField] private float focusDuration = 1f;
    [SerializeField] private float returnDuration = 1f;
    [SerializeField] private float targetSize = 0.5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private RectTransform target;

    [Header("事件")]
    public UnityEvent OnFocusStart;
    public UnityEvent OnFocusComplete;
    public UnityEvent OnReturnComplete;

    private Camera targetCamera;
    private Vector3 originalPosition;
    private float originalSize;
    private Quaternion originalRotation;

    public FocusState currentState = FocusState.Idle;
    private float transitionProgress;
    private Transform currentTarget;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float targetCamSize;

    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        CacheOriginalState();
    }

    private void Update()
    {
        if (currentState == FocusState.Idle) return;

        transitionProgress += Time.deltaTime / GetCurrentDuration();
        transitionProgress = Mathf.Clamp01(transitionProgress);

        switch (currentState)
        {
            case FocusState.Focusing:
                UpdateFocus();
                break;
            case FocusState.Returning:
                UpdateReturn();
                break;
            case FocusState.Waiting:
                UpdateWaiting();
                break;
        }
    }

    public void StartFocus()
    {
        currentTarget = target;
        CacheOriginalState();
        CalculateTargetPosition();
        transitionProgress = 0;
        currentState = FocusState.Focusing;
        OnFocusStart?.Invoke();
    }

    public void StartFocus(Transform transform)
    {
        currentTarget = transform;
        CacheOriginalState();
        CalculateTargetPosition();
        transitionProgress = 0;
        currentState = FocusState.Focusing;
        OnFocusStart?.Invoke();
    }

    private void UpdateFocus()
    {
        float t = Mathf.SmoothStep(0, 1, transitionProgress);

        transform.position = Vector3.Lerp(
            originalPosition,
            targetPosition,
            t
        );

        transform.rotation = Quaternion.Lerp(
            originalRotation,
            targetRotation,
            t
        );

        if (targetCamera.orthographic)
        {
            targetCamera.orthographicSize = Mathf.Lerp(
                originalSize,
                targetCamSize,
                t
            );
        }

        if (transitionProgress >= 1)
        {
            currentState = FocusState.Waiting;
            transitionProgress = 0;
            OnFocusComplete?.Invoke();
        }
    }

    private void UpdateReturn()
    {
        Debug.Log("Return");

        float t = Mathf.SmoothStep(0, 1, transitionProgress);

        transform.position = Vector3.Lerp(
            targetPosition,
            originalPosition,
            t
        );

        transform.rotation = Quaternion.Lerp(
            targetRotation,
            originalRotation,
            t
        );

        if (targetCamera.orthographic)
        {
            targetCamera.orthographicSize = Mathf.Lerp(
                targetCamSize,
                originalSize,
                t
            );
        }

        if (transitionProgress >= 1)
        {
            currentState = FocusState.Idle;
            OnReturnComplete?.Invoke();
        }
    }

    private void UpdateWaiting()
    {
        if (transitionProgress >= 1)
        {
            //currentState = FocusState.Returning;
            gameObject.GetComponent<CameraDrag>().enabled = true;
            transitionProgress = 0;
        }
    }

    public void OnStartReturn()
    {
        currentState = FocusState.Returning;
        gameObject.GetComponent<CameraDrag>().enabled = false;
    }


    private void CalculateTargetPosition()
    {
        Vector3 worldPos = GetUIWorldPosition(currentTarget as RectTransform);
        targetPosition = worldPos + offset;
        targetRotation = Quaternion.identity;
        targetCamSize = targetSize;
    }

    private float GetCurrentDuration()
    {
        return currentState switch
        {
            FocusState.Focusing => focusDuration,
            FocusState.Returning => returnDuration,
            FocusState.Waiting => 1f, // 等待时间
            _ => 1f
        };
    }

    // 以下方法与原始版本保持不变
    private void CacheOriginalState()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalSize = targetCamera.orthographicSize;
    }

    private Vector3 GetUIWorldPosition(RectTransform uiElement)
    {
        // 保持原有实现
        Canvas canvas = GetTopmostCanvas(uiElement);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(
            canvas.worldCamera,
            uiElement.position
        );
        return canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? screenPoint
            : canvas.worldCamera.ScreenToWorldPoint(screenPoint);
    }

    private Canvas GetTopmostCanvas(Transform transform)
    {
        Canvas parentCanvas = transform.GetComponentInParent<Canvas>();
        return parentCanvas.rootCanvas;
    }

    public void ImmediateReturn()
    {
        currentState = FocusState.Idle;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        targetCamera.orthographicSize = originalSize;
    }
}