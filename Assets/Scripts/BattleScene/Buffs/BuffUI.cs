using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI AmountText;

    BuffBehaviour buffBehaviour;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DialogueManager.Instance.ShowCursorInfo(buffBehaviour.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DialogueManager.Instance.HideCursorInfo();
    }

    public void SetAmount(string amount)
    {
        AmountText.text = amount;
    }

    public void ShowBuffManual()
    {
        // ManualPanel.ShowPanel(7);
    }

    void Awake()
    {
        buffBehaviour = GetComponent<BuffBehaviour>();
        GetComponent<Image>().raycastTarget = false;
        AmountText.raycastTarget = false;
    }
}
