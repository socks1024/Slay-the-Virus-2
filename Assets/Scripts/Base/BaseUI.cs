using SaveAndLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BaseUI : MonoBehaviour
{
    public TMPro.TMP_Text NameText;
    public TMPro.TMP_Text Life;
    public TMPro.TMP_Text DateText;
    public TMPro.TMP_Text Weekdaytext;
    public TMPro.TMP_Text Nut;

    private void Awake()
    {
        UpdateInfo();

        if (SaveSystem.Instance.getSave().TutorialClear[0] == false)
        {
            DialogueManager.Instance.ShowDialoguePanel().AddDialogueEvent(DialogueManager.Instance.loader, "base1").ShowNextDialogueEvent();
            SaveSystem.Instance.SetTutorialClear(0);
            Debug.Log("Tutorial");
        }

        if (SaveSystem.Instance.getSave().TutorialClear[0] == true&&SaveSystem.Instance.getSave().TutorialClear[2] == true && SaveSystem.Instance.getSave().TutorialClear[3] == false)
        {
            DialogueManager.Instance.ShowDialoguePanel().AddDialogueEvent(DialogueManager.Instance.loader, "base2").ShowNextDialogueEvent();
            SaveSystem.Instance.SetTutorialClear(3);
        }
        ItemManager.Instance.InitStorageCard();
    }

    private void UpdateInfo()
    {
        
        string datetext= System.DateTime.Now.ToShortDateString();
        char[] newdatetext=new char[11];
        for(int i = 0; i < datetext.Length; i++)
        {
            if (datetext[i] == '/')
            {
                newdatetext[i] = '.';
            }
            else
            {
                newdatetext[i] = datetext[i];
            }
        }
        datetext = new string(newdatetext);
        DateText.text = datetext;

        string week = System.DateTime.Now.DayOfWeek.ToString();
        switch (week)
        {
            case "Monday":
                Weekdaytext.text = "Mon.";
                break;
            case "Tuesday":
                Weekdaytext.text = "Tue.";
                break;
            case "Wednesday":
                Weekdaytext.text = "Wed.";
                break;
            case "Thursday":
                Weekdaytext.text = "Thu.";
                break;
            case "Friday":
                Weekdaytext.text = "Fri.";
                break;
            case "Saturday":
                Weekdaytext.text = "Sat.";
                break;
            case "Sunday":
                Weekdaytext.text = "Sun.";
                break;

        }

        //Debug.Log(SaveSystem.Instance.getSave().Name);
        NameText.text = SaveSystem.Instance.getSave().Name;
        Life.text = SaveSystem.Instance.getSave().BaseLife.ToString();
        Nut.text = SaveSystem.Instance.getSave().Nutrient.ToString();

    }

    public void TransToInventory()
    {
        if(SaveSystem.Instance.getSave().TutorialClear[2] == true)
            SceneManager.LoadScene("Inventory");
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Destroy(GameObject.Find("Managers"));
            SceneManager.LoadScene("Start");
        }
    }
}
