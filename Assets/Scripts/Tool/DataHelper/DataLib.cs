using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLib<T> : BaseSingleton<DataLib<T>> where T : Object
{
    public static Dictionary<string, T> DataDic = new();

    public static string ResourcesPath;

    [RuntimeInitializeOnLoadMethod]
    static void LoadData()
    {
        T[] resources = Resources.LoadAll<T>(ResourcesPath);
        foreach (T resource in resources)
        {
            DataDic.Add("", resource);
        }
    }
}
