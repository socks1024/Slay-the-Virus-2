using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System.IO;
using UnityEngine.AI;
using System.Security.Cryptography;
using JetBrains.Annotations;



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

        //Debug.Log(path);
        File.WriteAllText(path, json);
    }

    public PlayerSave getSave()
    {
        return savefile;
    }//��ȡ���ڵĴ浵

    public void SetSave(PlayerSave playerSave)
    {
        savefile = playerSave;
    }

    public void LevelClear(int index)
    {
        if (savefile!=null)
        {
            savefile.ClearLevels[index] = true;
        }
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
        //Debug.Log(GetSlotPath(index));
        return File.Exists(GetSlotPath(index));
    }

    public void AddCardToPlayerSave(string name, int amount)//��ֿ����ĳ����
    {
        if (savefile != null)
        {
            if (savefile.PlayerCardInventory.ContainsKey(name))
            {
                savefile.PlayerCardInventory[name] += amount;
                if (savefile.PlayerCardInventory[name] < 0)
                {
                    savefile.PlayerCardInventory[name] = 0;
                }

                int num = savefile.PlayerCardInventory[name];
                if (savefile.PlayerHoldCards.ContainsKey(name))
                {
                    num += savefile.PlayerHoldCards[name];
                }

                num -= 99;
                if (num < 0)
                    num = 0;
                savefile.PlayerCardInventory[name] -= num;
                
            }
            else
            {
                savefile.PlayerCardInventory.Add(name, amount);
            }
        }

        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void AddNutrientToPlayerSave(int amount)//���ӻ��ң����븺�����٣�����0��
    {
        if (savefile != null)
        {
            savefile.Nutrient += amount;
            if (savefile.Nutrient < 0)
            {
                savefile.Nutrient = 0;
            }
        }
        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void SetLifeOfPlayerSave(int num)//���û�������Ϊ����int
    {
        if (savefile != null)
        {
            savefile.BaseLife = num;
        }
        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void AddLifeofPlayerSave(int num)//���ӻ������������븺�����٣�����0��
    {
        if (savefile != null)
        {
            savefile.BaseLife += num;
        }
        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void ResetPlayerHoldCards(Dictionary<string, int> newcards) //������ҳ��п��飬����string int �Ĵʵ�
    {
        if (savefile != null)
        {
            savefile.PlayerHoldCards.Clear();
            foreach(var card in newcards)
            {
                savefile.PlayerHoldCards.Add(card);
            }
        }

        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void ResetPlayerHoldCards() //������ҳ��п��飨ֱ����գ�
    {
        if (savefile != null)
        {
            savefile.PlayerHoldCards.Clear();
        }

        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void AddPlayerHoldCards(string name,int amount)//����ҳ��п�����ӿ��ƣ����븺�����٣�����Ϊ0����ʱ�Ƴ���
    {
        if (savefile != null)
        {
            if (savefile.PlayerHoldCards.ContainsKey(name))
            {
                savefile.PlayerHoldCards[name] += amount;
                if (savefile.PlayerHoldCards[name] <= 0)
                {
                    savefile.PlayerHoldCards.Remove(name);
                }
            }
        }
        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void SetTutorialClear(int index)//�̳̽���
    {
        if (savefile != null)
        {
            savefile.TutorialClear[index] = true;
        }

        SavePlayerToSlot(savefile, savefile.saveindex);
    }

    public void AddPlayerHoldCardsFromInventory(string name,int amount)
    {
        if (savefile != null)
        {
            savefile.PlayerCardInventory[name] -= amount;

            if (savefile.PlayerHoldCards.ContainsKey(name))
            {
                savefile.PlayerHoldCards[name] += amount;
                //Debug.Log(name);
            }
            else
            {
                savefile.PlayerHoldCards.Add(name, amount);
                Debug.Log(name);
            }
        }

        SavePlayerToSlot(savefile, savefile.saveindex);
    }
}


[System.Serializable]
public class PlayerSave//�浵�����������Ϣ��ͨ����ȡSaveSystem�µ�GetSave��ȡ
{
    public string Name;  //�����
    public string Time;  //����ʱ��
    public string birthTime;  //��д������
    public int gender;  //�Ա� 0=δ֪ 1=�� 2=Ů
    public bool[] illness = new bool[6];  //�Ƿ�ѡ��Ӧ���� 

    public int BaseLife;  //����
    public int Nutrient;  //Ӫ��

    public bool[] ClearLevels = new bool[6]; //��ͨ���Ĺؿ�

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
        {"WarShip",0}
    };  //��Ҳֿ�Ŀ���

    public SerializableDictionary<string, int> PlayerHoldCards = new SerializableDictionary<string, int>();  //��ҿ�����Ŀ���

    public int saveindex;  //�浵���

    public bool[] TutorialClear = new bool[]{ 
       false,
       false,
       false,
       false
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

