using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System.IO;
using UnityEngine.AI;



public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private PlayerSave savefile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    public void SavePlayerToSlot(PlayerSave player,int index)
    {
        player.Time = System.DateTime.Now.ToShortDateString();
        string json = JsonUtility.ToJson(player,true);
        string path = GetSlotPath(index);

        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    public PlayerSave getSave()
    {
        return savefile;
    }

    public void SetSave(PlayerSave playerSave)
    {
        savefile = playerSave;
    }

    public PlayerSave LoadPlayerFromSlot(int slotIndex)
    {
        string path = GetSlotPath(slotIndex);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            PlayerSave loadData = JsonUtility.FromJson<PlayerSave>(json);

            return loadData;
        }
        else
        {
            return null;
        }
    }

    private string GetSlotPath(int index)
    {
        return Path.Combine(Application.persistentDataPath, $"saveSlot_{index}.json");
    }

    public SaveInfo GetSaveInfo(int index)
    {
        string path = GetSlotPath(index);
        if (!File.Exists(path))
            return new SaveInfo();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveInfo>(json);
    }

    public void DeleteSlot(int index)
    {
        string path = GetSlotPath(index);
        if (File.Exists(path)) 
        {
            File.Delete(path);
        }
    }

    public bool SlotFileExist(int index)
    {
        return File.Exists(GetSlotPath(index));
    }

}


[System.Serializable]
public class PlayerSave
{
    public string Name;
    public string Time;
    public string birthTime;
    public int gender;
    public bool[] illness = new bool[6];

    public int BaseLife;
    public int Nutrient;

    public SerializableDictionary<string, int> PlayerCardInventory = new SerializableDictionary<string, int>
    {
        {"BazookaSoldier",0 },
        {"ElectricGrenade",0 }
    };
   
}

[System.Serializable]
public class SaveInfo
{
    public string name;
    public string time;

    public SaveInfo() { }

    public SaveInfo(PlayerSave savedata)
    {
        if (savedata == null)
            return;
        name = savedata.Name;
        time = savedata.Time;
    }
}
