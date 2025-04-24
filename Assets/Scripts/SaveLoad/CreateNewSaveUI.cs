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
        "头晕头痛恶心",
        "尿频尿急尿失禁",
        "肌肉酸痛麻木",
        "呕吐食欲不振",
        "鼻涕流涕咽痛抽搐",
        "视力模糊听力下降"
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

        savedata.BaseLife = 25;//默认值
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
                GenderText.text = "未知";
                break;
            case 1:
                GenderText.text = "男";
                break;
            case 2:
                GenderText.text = "女";
                break;
        }

        bool AllFalse = true;
        string destext = "患者描述有";
        for(int i = 0; i < 6; i++)
        {
            if (illnesses[i].isOn == true)
            {
                if (AllFalse == false)
                {
                    destext += "、";
                }
                destext += illnessdes[i];
                AllFalse = false;
            }
        }

        if (AllFalse)
        {
            DescriptionText.text = "患者没描述任何病情，未查明来访原因，建议患者转至精神科就诊。";
        }
        else
        {
            destext += "。暂未诊断出具体原因，推荐患者回家观察疗养三日。";
            DescriptionText.text = destext;
        }
    }
}
