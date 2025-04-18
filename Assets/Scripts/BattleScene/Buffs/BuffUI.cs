using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AmountText;

    public void SetAmount(string amount)
    {
        AmountText.text = amount;
    }
}
