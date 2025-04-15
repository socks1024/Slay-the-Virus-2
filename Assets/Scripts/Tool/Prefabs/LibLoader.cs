using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public static class LibLoader
// {
//     static List<PrefabLib> libs = new();

//     [RuntimeInitializeOnLoadMethod]
//     static void LoadAllPrefabs()
//     {
//         foreach (PrefabLib lib in libs)
//         {
            
//         }
//     }

//     public static void CreateNewLib<T>(string path) where T : MonoBehaviour
//     {
//         libs.Add(new PrefabLib<T>(path));
//     }

//     public static List<T> ReadLib<T>()
//     {
//         List<T> list = new();

//         return list;
//     }
// }

// public class PrefabLib<T> where T : MonoBehaviour
// {
//     public Dictionary<string, T> prefabs = new();

//     public string path;

//     public bool loadFinished = false;

//     public PrefabLib(string path)
//     {
//         this.path = path;
//     }

//     public void LoadSpecificPrefabs()
//     {
//         T[] resources = Resources.LoadAll<T>(path);
//         foreach (T resource in resources)
//         {
//             prefabs.Add(resource.gameObject.name, resource);
//         }
//         loadFinished = true;
//     }
// }
