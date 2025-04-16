using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public static Vector2 Rotate2DVector(Vector2 vector, float angle)
    {
        // // 将角度转换为弧度
        // float radians = angle * Mathf.Deg2Rad;

        // // 计算旋转后的向量
        // float cosTheta = Mathf.Cos(radians);
        // float sinTheta = Mathf.Sin(radians);

        // float x = vector.x * cosTheta - vector.y * sinTheta;
        // float y = vector.x * sinTheta + vector.y * cosTheta;

        Vector2 vec = Quaternion.Euler(0,0,angle) * vector;

        return vec;
    }

    public static Vector2 Rotate2DVectorInt(Vector2 vector, float angle)
    {
        // // 将角度转换为弧度
        // float radians = angle * Mathf.Deg2Rad;

        // // 计算旋转后的向量
        // float cosTheta = Mathf.Cos(radians);
        // float sinTheta = Mathf.Sin(radians);

        // float x = vector.x * cosTheta - vector.y * sinTheta;
        // float y = vector.x * sinTheta + vector.y * cosTheta;

        Vector2 vec = Quaternion.Euler(0,0,angle) * vector;

        vec.x = Mathf.RoundToInt(vec.x);
        vec.y = Mathf.RoundToInt(vec.y);

        return vec;
    }
}
