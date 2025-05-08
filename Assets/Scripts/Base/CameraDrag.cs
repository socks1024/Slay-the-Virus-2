using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Tooltip("控制拖拽移动的速度")]
    public float dragSpeed = 0.01f;

    [Tooltip("移动平滑速度")]
    public float smoothSpeed = 5f;

    [Tooltip("允许移动的最小Y坐标")]
    public float minY = 0f;

    [Tooltip("允许移动的最大Y坐标")]
    public float maxY = 20f;

    private float _lastMouseY;
    private bool _isDragging;
    private Vector3 _targetPosition;

    void Start()
    {
        // 初始化目标位置为当前位置
        _targetPosition = transform.position;
    }


    void Update()
    {
        HandleInput();
        SmoothMovement();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }

        if (_isDragging)
        {
            CalculateTargetPosition();
        }
    }

    void SmoothMovement()
    {
        // 使用Lerp平滑移动到目标位置
        transform.position = Vector3.Lerp(
            transform.position,
            _targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    void CalculateTargetPosition()
    {
        float currentMouseY = Input.mousePosition.y;
        float deltaY = currentMouseY - _lastMouseY;

        // 计算原始目标位置
        Vector3 newPosition = _targetPosition +
                            Vector3.up * (deltaY * dragSpeed*-1);

        // 应用范围限制
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // 更新最终目标位置
        _targetPosition = newPosition;
        _lastMouseY = currentMouseY;
    }


    void StartDragging()
    {
        _lastMouseY = Input.mousePosition.y;
        _isDragging = true;
    }

    void StopDragging()
    {
        _isDragging = false;
    }

    void HandleDragMovement()
    {
        float currentMouseY = Input.mousePosition.y;
        float deltaY = (currentMouseY - _lastMouseY);

        // 计算新位置
        Vector3 newPosition = transform.position +
                            Vector3.up * (deltaY * dragSpeed);

        // 应用范围限制
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // 更新摄像机位置
        transform.position = newPosition;

        _lastMouseY = currentMouseY;
    }
}
