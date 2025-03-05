using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class RandomHelper
{
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder sb = new StringBuilder();
        System.Random random = new System.Random();

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(chars.Length); // 随机选择一个字符的索引
            sb.Append(chars[index]); // 将字符添加到 StringBuilder
        }

        return sb.ToString();
    }
}
