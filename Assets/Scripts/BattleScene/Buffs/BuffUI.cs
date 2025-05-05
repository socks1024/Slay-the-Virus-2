using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AmountText;

    public void SetAmount(string amount)
    {
        AmountText.text = amount;
    }

    public void ShowBuffManual()
    {
        ManualPanel.ShowPanel(7);
    }

    void Awake()
    {
        GetComponent<Image>().raycastTarget = false;
        AmountText.raycastTarget = false;
    }
}
