using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public int index;
    public GameObject File;
    public GameObject Create;
    public GameObject DeletePanel;

    private TMPro.TMP_Text Name;
    private TMPro.TMP_Text time;
    private TMPro.TMP_Text Num;

    public void Start()
    {
        index = transform.GetSiblingIndex();
        Create = transform.GetChild(0).gameObject;
        File = transform.GetChild(1).gameObject;

        if (SaveSystem.Instance.SlotFileExist(index))
        {
            PlayerSave player = SaveSystem.Instance.LoadPlayerFromSlot(index);
            Create.SetActive(false);
            File.SetActive(true);

            Name = File.transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
            Name.text = player.Name;

            time = File.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>();
            time.text = player.Time;

            Num = File.transform.GetChild(2).gameObject.GetComponent<TMPro.TMP_Text>();
            Num.text = $"No.{index+1}";

        }
        else
        {
            File.SetActive(false);
            Create.SetActive(true);
        }
    }

    public void StartCreate()
    {
        GetComponentInParent<StartPanel>().Create(index);
    }

    public void LoadSave()
    {
        SaveSystem.Instance.SetSave(SaveSystem.Instance.LoadPlayerFromSlot(index));

        SceneManager.LoadScene("Base");
    }

    public void OnDeleteSave()
    {
        DeletePanel.SetActive(true);
        DeletePanel.GetComponent<DeleteSave>().index = index;
;    }
    
}
