using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewSaveUI : MonoBehaviour
{
    public Toggle[] illnesses;
    public TMPro.TMP_InputField nametext;
    public TMPro.TMP_InputField birthtext;
    public TMPro.TMP_Dropdown gender;
    public int thisindex;
    public GameObject panel1;
    public GameObject confirmpanel;
    public TMPro.TMP_Text NameText;
    public TMPro.TMP_Text GenderText;
    public TMPro.TMP_Text BirthText;
    public TMPro.TMP_Text DescriptionText;

    private string[] illnessdes =
    {
        "ͷ��ͷʹ����",
        "��Ƶ����ʧ��",
        "������ʹ��ľ",
        "Ż��ʳ������",
        "����������ʹ�鴤",
        "����ģ�������½�"
    };

    public void Start()
    {
        confirmpanel.SetActive(false);
        panel1.SetActive(true);
    }
    private void CreateNewSave(int index)
    {
        PlayerSave savedata=new PlayerSave();
        savedata.Name = nametext.text;
        savedata.birthTime = birthtext.text;
        savedata.gender = gender.value;

        savedata.BaseLife = 25;//Ĭ��ֵ
        savedata.Nutrient = 0;

        for(int i = 0; i < 6; i++)
        {
            savedata.illness[i] = illnesses[i].isOn;
        }

        savedata.PlayerCardInventory["BazookaSoldier"] = 5;
        savedata.PlayerCardInventory["ElectricGrenade"] = 5;


        if (nametext.text == "JCY")
        {
           foreach (var key in savedata.PlayerCardInventory.Keys.ToList())
            {
                savedata.PlayerCardInventory[key] = 99;
            }

            savedata.BaseLife = 1000;
            savedata.Nutrient = 1000;
        }
      

        SaveSystem.Instance.SavePlayerToSlot(savedata, index);
    }

    public void StartCreate()
    {
        CreateNewSave(thisindex);
        panel1.SetActive(false);
        confirmpanel.SetActive(false);
        gameObject.SetActive(false);
        GetComponentInParent<StartPanel>().updateslots();
    }

    public void TransformPanel()
    {
        panel1.SetActive(false);
        SetConfirmPanel();
        confirmpanel.SetActive(true);
        this.GetComponent<Image>().enabled = false;
    }

    private void SetConfirmPanel()
    {
        NameText.text = nametext.text;
        BirthText.text = birthtext.text;

        switch (gender.value)
        {
            case 0:
                GenderText.text = "δ֪";
                break;
            case 1:
                GenderText.text = "��";
                break;
            case 2:
                GenderText.text = "Ů";
                break;
        }

        bool AllFalse = true;
        string destext = "����������";
        for(int i = 0; i < 6; i++)
        {
            if (illnesses[i].isOn == true)
            {
                if (AllFalse == false)
                {
                    destext += "��";
                }
                destext += illnessdes[i];
                AllFalse = false;
            }
        }

        if (AllFalse)
        {
            DescriptionText.text = "����û�����κβ��飬δ��������ԭ�򣬽��黼��ת������ƾ��";
        }
        else
        {
            destext += "����δ��ϳ�����ԭ���Ƽ����߻ؼҹ۲��������ա�";
            DescriptionText.text = destext;
        }
    }
}
