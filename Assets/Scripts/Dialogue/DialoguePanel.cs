using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] RectTransform textPanel;

    [SerializeField] Image headImage;

    [SerializeField] Image panelImage;

    [SerializeField] TextMeshProUGUI lineText;

    [SerializeField] TextMeshProUGUI nameText;

    Sprite defaultHead;

    Sprite defaultPanel;

    Queue<DialogueEvent> dialogueEventsQueue = new();

    void Awake()
    {
        defaultHead = headImage.sprite;
        defaultPanel = panelImage.sprite;

        GetComponentsInChildren<Image>().ToList().ForEach(g => g.raycastTarget = false);
        GetComponentsInChildren<TextMeshProUGUI>().ToList().ForEach(g => g.raycastTarget = false);
        GetComponent<Image>().raycastTarget = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerClick();
        }
    }

    public void OnPointerClick()
    {
        if (!typing)
        {
            if (dialogueEventsQueue.Count == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                ShowNextDialogueEvent();
            }
        }
        else
        {
            EndType();
        }
    }

    #region Setters

    public DialoguePanel SetTextPanelPosition(TextPanelPosition position)
    {
        if (position == TextPanelPosition.DEFAULT)
        {
            // don't move
        }
        if (position == TextPanelPosition.UP)
        {
            textPanel.pivot = new Vector2(0.5f,1);

            textPanel.anchorMin = new Vector2(0,1);
            textPanel.anchorMax = new Vector2(1,1);

            textPanel.gameObject.SetActive(true);
        }
        if (position == TextPanelPosition.MIDDLE)
        {
            textPanel.pivot = new Vector2(0.5f,0.5f);

            textPanel.anchorMin = new Vector2(0,0.5f);
            textPanel.anchorMax = new Vector2(1,0.5f);

            textPanel.gameObject.SetActive(true);
        }
        if (position == TextPanelPosition.DOWN)
        {
            textPanel.pivot = new Vector2(0.5f,0);

            textPanel.anchorMin = new Vector2(0,0);
            textPanel.anchorMax = new Vector2(1,0);

            textPanel.gameObject.SetActive(true);
        }
        if (position == TextPanelPosition.HIDE)
        {
            textPanel.gameObject.SetActive(false);
        }

        return this;
    }

    void AddEvent(DialogueEvent dialogueEvent)
    {
        if (dialogueEvent.head is null) dialogueEvent.head = defaultHead;
        if (dialogueEvent.panel is null) dialogueEvent.panel = defaultPanel;

        dialogueEventsQueue.Enqueue(dialogueEvent);
    }

    public DialoguePanel AddDialogueEvent(IDialogueLoader loader, int ID)
    {
        DialogueEvent dialogueEvent = loader.LoadDialogueEvent(ID);

        AddEvent(dialogueEvent);

        return this;
    }

    public DialoguePanel AddDialogueEvent(IDialogueLoader loader, int[] IDs)
    {
        for (int i = 0; i < IDs.Length; i++)
        {
            AddDialogueEvent(loader, IDs[i]);
        }

        return this;
    }

    public DialoguePanel AddDialogueEvent(IDialogueLoader loader, string groupID)
    {
        foreach (DialogueEvent dialogueEvent in loader.LoadDialogueEventsByGroup(groupID))
        {
            AddEvent(dialogueEvent);
        }

        return this;
    }

    #endregion

    #region Typing

    public float speed = 10;

    [HideInInspector] public string currLine;

    bool typing = false;

    IEnumerator TypeLine()
    {
        float time = 0;

        int index = 0;

        while(index < currLine.Length)
        {
            time += Time.deltaTime * speed;

            if (currLine[index] == '<')
            {

                do 
                {
                    time += 1;

                    index = Mathf.FloorToInt(time);
                } 
                while (currLine[index - 1] != '>');
            }

            index = Mathf.FloorToInt(time);

            index = Mathf.Clamp(index, 0, currLine.Length);

            lineText.text = currLine.Substring(0,index);

            yield return null;
        }

        typing = false;
    }

    void StartType()
    {
        StartCoroutine("TypeLine");
        typing = true;
    }

    void EndType()
    {
        StopCoroutine("TypeLine");
        typing = false;
        lineText.text = currLine;
    }

    #endregion
    
    public void ShowNextDialogueEvent()
    {
        DialogueEvent dialogueEvent = dialogueEventsQueue.Dequeue();

        nameText.text = dialogueEvent.name;
        currLine = dialogueEvent.line;
        headImage.sprite = dialogueEvent.head;
        panelImage.sprite = dialogueEvent.panel;

        SetTextPanelPosition(dialogueEvent.textPanelPosition);

        StartType();
    }
}

public struct DialogueEvent
{
    public string name;

    public string line;

    public Sprite head;

    public Sprite panel;

    public TextPanelPosition textPanelPosition;
}

public enum TextPanelPosition
{
    DEFAULT,
    DOWN,
    UP,
    MIDDLE,
    HIDE,
}
