using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPanel : MonoBehaviour
{
    static ManualPanel manualPanel;

    public static void ShowPanel()
    {
        manualPanel.gameObject.SetActive(true);
        manualPanel.CurrContentIndex = 0;
    }

    public static void ShowPanel(int index)
    {
        manualPanel.gameObject.SetActive(true);
        manualPanel.CurrContentIndex = index;
    }

    void Awake()
    {
        manualPanel = this;
        gameObject.SetActive(false);
    }

    public void HidePanel()
    {
        currContent.SetActive(false);
        gameObject.SetActive(false);
    }

    [SerializeField] List<GameObject> contents = new();

    GameObject currContent;

    public int CurrContentIndex
    {
        get
        {
            return currContentIndex;
        }
        
        set
        {
            if (value >= contents.Count) value = 0;
            if (value < 0) value = contents.Count - 1;
            currContentIndex = value;
            ShowContent(currContentIndex);
        }
    }
    int currContentIndex;

    void ShowContent(int index)
    {
        currContent?.SetActive(false);

        currContent = contents[index];

        currContent.SetActive(true);
    }

    public void ShowPrevContent()
    {
        CurrContentIndex -= 1;
    }

    public void ShowNextContent()
    {
        CurrContentIndex += 1;
    }
}
