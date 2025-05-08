using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Tooltip("������ק�ƶ����ٶ�")]
    public float dragSpeed = 0.01f;

    [Tooltip("�ƶ�ƽ���ٶ�")]
    public float smoothSpeed = 5f;

    [Tooltip("�����ƶ�����СY����")]
    public float minY = 0f;

    [Tooltip("�����ƶ������Y����")]
    public float maxY = 20f;

    private float _lastMouseY;
    private bool _isDragging;
    private Vector3 _targetPosition;

    void Start()
    {
        // ��ʼ��Ŀ��λ��Ϊ��ǰλ��
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
        // ʹ��Lerpƽ���ƶ���Ŀ��λ��
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

        // ����ԭʼĿ��λ��
        Vector3 newPosition = _targetPosition +
                            Vector3.up * (deltaY * dragSpeed*-1);

        // Ӧ�÷�Χ����
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // ��������Ŀ��λ��
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

        // ������λ��
        Vector3 newPosition = transform.position +
                            Vector3.up * (deltaY * dragSpeed);

        // Ӧ�÷�Χ����
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // ���������λ��
        transform.position = newPosition;

        _lastMouseY = currentMouseY;
    }
}
