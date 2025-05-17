using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CursorInfoPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    public void SetText(string input)
    {
        tmp.text = input;
    }

    void OnEnable()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = GetComponentInParent<Canvas>().transform.position.z;
        transform.position = p;
    }

    void Update()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = GetComponentInParent<Canvas>().transform.position.z;
        transform.position = p;
    }
}
