using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public GameObject CreateNewFilePanel;
    public GameObject[] SaveSlots = new GameObject[3];

    private void Start()
    {
        CreateNewFilePanel.SetActive(false);
    }

    public void updateslots()
    {
        for(int i = 0; i < 3; i++)
        {
            SaveSlots[i].GetComponent<SaveSlot>().Start();
        }
       // CreateNewFilePanel.SetActive(false);
    }

    public void Create(int index)
    {
        CreateNewFilePanel.SetActive(true);
        CreateNewFilePanel.GetComponent<CreateNewSaveUI>().thisindex = index;
        CreateNewFilePanel.GetComponent<Image>().enabled = true;
        CreateNewFilePanel.GetComponent<CreateNewSaveUI>().Start();
    }
}
