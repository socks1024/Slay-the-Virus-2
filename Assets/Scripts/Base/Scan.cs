using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public GameObject[] RedDots;

    private void Start()
    {
        for(int i = 0; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(false);
            RedDots[i].transform.parent.GetChild(RedDots[i].transform.childCount + 1).gameObject.SetActive(false);
        }
    }

    public void SetActive()
    {
        for (int i = 0; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(true);
        }
    }

}
