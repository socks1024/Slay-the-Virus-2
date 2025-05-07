using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Tooltip("控制拖拽移动的速度")]
    public float dragSpeed = 0.01f;

    [Tooltip("允许移动的最小Y坐标")]
    public float minY = 0f;

    [Tooltip("允许移动的最大Y坐标")]
    public float maxY = 20f;

    private float _lastMouseY;
    private bool _isDragging;

    void Update()
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
            HandleDragMovement();
        }
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
        float deltaY = currentMouseY - _lastMouseY;

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
