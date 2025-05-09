using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class HeadChange : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    public Sprite sprite0;
    public UnityEngine.UI.Image image;

    private TMPro.TMP_Dropdown _Dropdown;

    private void Start()
    {
        _Dropdown = GetComponent<TMPro.TMP_Dropdown>();

        //_Dropdown.onValueChanged.AddListener(OnValueChanged());
    }



    public void ChangeImage(int index)
    {
        switch (index)
        {
            case 0:
                image.sprite = sprite0;
                break;
            case 1:
                image.sprite = sprite1;
                break;
            case 2:
                image.sprite = sprite2;
                break;
        }
    }

    public void OnValueChanged()
    {
        switch (_Dropdown.value)
        {
            case 0:
                image.sprite = sprite0;
                break;
            case 1:
                image.sprite = sprite1;
                break;
            case 2:
                image.sprite = sprite2;
                break;
        }
    }
}
