using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCurve : MonoBehaviour
{
    public CurveMode curveMode = CurveMode.MULTIPLE_SPRITES;
    public Transform startPoint; // 卡牌位置（起点）
    public Transform endPoint;   // 鼠标/目标位置（终点）
    public LineRenderer lineRenderer;// 曲线的线状渲染器
    List<Transform> spriteRenderers;// 曲线的精灵渲染器
    public int curveResolution = 20; // 曲线分段数
    public float curveHeight = 1f;   // 曲线高度控制
    public Sprite sprite;// 曲线每一小段的图片

    void Start()
    {
        spriteRenderers = new();

        for (int i = 0; i < curveResolution - 2; i++)
        {
            Image sr = new GameObject().AddComponent<Image>();
            sr.transform.SetParent(transform, false);
            sr.sprite = sprite;
            sr.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0);
            spriteRenderers.Add(sr.transform);
        }
    }

    void Update()
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

        if (curveMode == CurveMode.LINE_RENDERER)
        {
            lineRenderer.gameObject.SetActive(true);
            // 更新LineRenderer
            lineRenderer.positionCount = curveResolution;
            lineRenderer.SetPositions(curvePoints);
        }
        else if (curveMode == CurveMode.MULTIPLE_SPRITES)
        {
            for (int i = spriteRenderers.Count - 1; i >= 0; i--)
            {
                spriteRenderers[i].position = curvePoints[i+1];
                SetPointDirection(spriteRenderers[i], curvePoints[i]);
            }
        }

        SetPointDirection(endPoint, curvePoints[curveResolution - 2]);
        endPoint.SetAsLastSibling();
    }

    // 二次贝塞尔曲线公式
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    void SetPointDirection(Transform transform, Vector3 lookAt)
    {
        Vector2 direction = (lookAt - transform.position).normalized;
        transform.up = direction;
    }
}

public enum CurveMode
{
    LINE_RENDERER,
    MULTIPLE_SPRITES,
}
