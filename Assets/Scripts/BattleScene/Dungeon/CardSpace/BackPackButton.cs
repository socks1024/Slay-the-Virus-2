using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackButton : MonoBehaviour
{
    public ShowDeckPanel panel;

    public void ShowBackPack()
    {
        panel.gameObject.SetActive(true);
        panel.Show();
    }
}
