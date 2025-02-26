using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public readonly static string filePathJson = Application.dataPath + "/SerializedFile" + "/SaveByJson.json";

    public static void SaveByJson<T>(T save) where T : ISerializable , new()
    {
        string saveJsonStr = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(filePathJson);
        sw.Write(saveJsonStr);
        sw.Close();
    }

    public static T LoadByJson<T>() where T : ISerializable , new()
    {
        T save= new T();

        if(File.Exists(filePathJson))
        {
            StreamReader sr = new StreamReader(filePathJson);
            string jsonStr = sr.ReadToEnd();
            sr.Close();

            save = JsonUtility.FromJson<T>(jsonStr);
        }

        return save;
    }
}

/// <summary>
/// 提醒我自己记得把要保存的东西加上[Serializable]标签。
/// </summary>
public interface ISerializable{}