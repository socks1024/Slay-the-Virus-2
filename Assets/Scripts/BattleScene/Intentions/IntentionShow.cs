using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IntentionShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;

    [SerializeField] TextMeshProUGUI textmesh;

    [SerializeField] List<IntentionDetail> intentionDetails = new();

    [HideInInspector] public IntentionDetail currIntention;


    public void ShowIntention()
    {
        image.raycastTarget = false;
        textmesh.raycastTarget = false;

        IntentionBehaviour intention = GetComponent<IntentionBehaviour>();

        textmesh.text = intention.ShowText;

        foreach (IntentionDetail data in intentionDetails)
        {
            if (intention.IntentionType == data.intentionType)
            {
                currIntention = data;
                image.sprite = data.sprite;
            }
        }
    }

    public void ShowIntentionManual()
    {
        // ManualPanel.ShowPanel(6);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DialogueManager.Instance.ShowCursorInfo(currIntention.intentionDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DialogueManager.Instance.HideCursorInfo();
    }
}

[Serializable]
public struct IntentionDetail
{
    public IntentionType intentionType;

    public Sprite sprite;

    public string intentionName;

    public string intentionDescription;
}
