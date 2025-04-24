using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialoguePanel : MonoBehaviour, IPointerClickHandler
{
    public RectTransform textPanel;

    public TextMeshProUGUI tmp;

    public UnityAction onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick is not null) onClick.Invoke();
        Destroy(gameObject);
    }

    public DialoguePanel SetOnClick(UnityAction action)
    {
        onClick = action;

        return this;
    }

    public DialoguePanel SetDialogueText(string tutorialText)
    {
        tmp.text = tutorialText;
        tmp.ForceMeshUpdate();

        return this;
    }

    public DialoguePanel SetTextPanelPosition(TextPanelPosition position)
    {
        if (position == TextPanelPosition.UP)
        {
            textPanel.pivot = new Vector2(0.5f,1);

            textPanel.anchorMin = new Vector2(0,1);
            textPanel.anchorMax = new Vector2(1,1);
        }
        else if (position == TextPanelPosition.MIDDLE)
        {
            textPanel.pivot = new Vector2(0.5f,0.5f);

            textPanel.anchorMin = new Vector2(0,0.5f);
            textPanel.anchorMax = new Vector2(1,0.5f);
        }
        else if (position == TextPanelPosition.DOWN)
        {
            textPanel.pivot = new Vector2(0.5f,0);

            textPanel.anchorMin = new Vector2(0,0);
            textPanel.anchorMax = new Vector2(1,0);
        }

        return this;
    }
}

public enum TextPanelPosition
{
    UP,
    MIDDLE,
    DOWN,
}
