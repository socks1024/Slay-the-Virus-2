using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermsPanel : MonoBehaviour
{
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
            if (value >= 0 && value < contents.Count) currContentIndex = value;
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
