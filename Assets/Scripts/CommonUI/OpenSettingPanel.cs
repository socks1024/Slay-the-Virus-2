using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettingPanel : MonoBehaviour
{
    public GameObject Setting;

    Button button;
    Button returnbutton;
    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        //Setting = GameObject.Find("SettingPanel");
        Setting.SetActive(false);

        returnbutton=Setting.transform.GetChild(2).gameObject.GetComponent<Button>();
        returnbutton.onClick.AddListener(OnReturn);
    }

    

    public void OnClick()
    {
        Setting.SetActive(true);
    }

    public void OnReturn()
    {
        Setting.SetActive(false);
    }
}
