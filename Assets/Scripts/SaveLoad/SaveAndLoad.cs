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
        Debug.Log(GetSlotPath(index));
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
        {"ElectricGrenade",0 },
        {"FlareGun",0 },
        {"PiercingBullet",0 },
        {"StunGrenade",0 },
        {"TearGas",0 },

        {"Grenade",0 },
        {"Interphone",0 },
        {"Map",0 },
        {"Medic",0 },
        {"Militia",0 },
        {"NoviceEngineer",0 },
        {"NoviceInfantry",0 },

        {"BodyArmor",0 },
        {"Knifeman",0 },
        {"Shocker",0 },
        {"SpikeArmor",0 },
        {"Supplies",0 },

        {"AttackFlag",0},
        {"DefenseFlag",0},
        {"EngineerGroup",0},
        {"InfantryGroup",0},
        {"SeniorEngineer",0},
        {"SeniorInfantry",0},

        {"Bandage",0},
        {"HeadNurse",0},
        {"RemoteSatellite",0},
        {"Substitute",0},
        {"SupplyDrop",0},
        {"Surveyor",0},

        {"Antibody",0},
        {"Antihistamine",0},
        {"BloodBag",0},
        {"Epinephrine",0},
        {"Herbs",0},
        {"Steroid",0},

        {"ArmouredCar",0},
        {"BarbedWire",0},
        {"Drone",0},
        {"Fortress",0},
        {"SimpleShelter",0},
        {"SpikeRoadblock",0},

        {"AccurateAirdrop",0},
        {"ArmorSoldier",0},
        {"DeathSquad",0},
        {"Kamikaze",0},
        {"Painkiller",0},
        {"RecklessWarrior",0},

        {"FightMaster",0},
        {"Guerrillas",0},
        {"MachineGunner",0},
        {"ShieldGuy",0},
        {"SystemEngineer",0},
        {"TacticalCommander",0},

        {"CloningFighters",0},
        {"CounterDummy",0},
        {"DismissalSeal",0},
        {"EmergencyCommunication",0},
        {"GasMask",0},
        {"TrainingDummy",0},

        {"Assassin",0},
        {"Barrier",0},
        {"RoadExplosion",0},
        {"Spy",0},
        {"Thief",0},
        {"Trap",0},

        {"AirBombing",0},
        {"Cannon",0},
        {"Jet",0},
        {"RedButton",0},
        {"Tank",0},
        {"Warship",0}
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

