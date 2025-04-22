using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCurve : MonoBehaviour
{
    public Transform startPoint; // 卡牌位置（起点）
    public Transform endPoint;   // 鼠标/目标位置（终点）
    public LineRenderer lineRenderer;
    public int curveResolution = 20; // 曲线分段数
    public float curveHeight = 1f;   // 曲线高度控制

    private void Update()
    {
        if (startPoint == null || endPoint == null) return;

        // 动态计算控制点（二次贝塞尔曲线）
        Vector3 controlPoint = (startPoint.position + endPoint.position) / 2 + Vector3.up * curveHeight;

        // 生成曲线点
        Vector3[] curvePoints = new Vector3[curveResolution];
        for (int i = 0; i < curveResolution; i++)
        {
            float t = i / (float)(curveResolution - 1);
            curvePoints[i] = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint, endPoint.position);
        }

        // 更新LineRenderer
        lineRenderer.positionCount = curveResolution;
        lineRenderer.SetPositions(curvePoints);
    }

    // 二次贝塞尔曲线公式
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}
