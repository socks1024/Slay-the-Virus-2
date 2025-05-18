using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSets : MonoBehaviour
{
    public Sprite chosen;
    public Sprite unchosen;

    public Image[] buttons;

    public void SetButtonSprite(int index)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].sprite = unchosen;
        }

        buttons[index].sprite = chosen;
    }
}
