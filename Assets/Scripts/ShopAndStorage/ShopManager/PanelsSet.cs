using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelsSet : MonoBehaviour
{
    public List<GameObject> panels=new List<GameObject>();

    public void Awake()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
    }

    public void SetPanelActive(GameObject thepanel)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        thepanel.SetActive(true);
    }
}
