using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameText;

    [SerializeField] TextMeshProUGUI DateText;

    [SerializeField] GameObject SettingsPanel;

    void Start()
    {
        NameText.text = SaveSystem.Instance.getSave().Name;

        DateText.text = DateTime.Now.ToString("MM/dd/yyyy");
    }

    public void EnterSettingsPanel()
    {
        SettingsPanel.gameObject.SetActive(true);
    }
}
