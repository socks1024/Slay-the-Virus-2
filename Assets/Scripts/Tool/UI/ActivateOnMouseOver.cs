using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
    public GameObject target;

    private void Start()
    {
        this.target.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.target.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.target.SetActive(false);
    }

    private void OnDisable()
    {
        this.target.SetActive(false);
    }
}
