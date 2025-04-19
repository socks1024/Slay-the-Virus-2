using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameText;

    [SerializeField] TextMeshProUGUI DateText;

    void Start()
    {
        NameText.text = SaveSystem.Instance.getSave().Name;

        DateText.text = DateTime.Now.ToString("MM/dd/yyyy");
    }
}
