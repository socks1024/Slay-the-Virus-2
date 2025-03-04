using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeepCopy
{
    public static List<T> DeepCopyValueTypeList<T>(List<T> originalList)
    {
        List<T> list = new List<T>();

        foreach (T item in originalList)
        {
            
            list.Add(item);
        }

        return list;
    }
}
