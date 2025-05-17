using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField] DialoguePanel PanelPrefab;

    [SerializeField] CursorInfoPanel InfoPanelPrefab;

    public IDialogueLoader loader;

    DialoguePanel dialoguePanel;

    CursorInfoPanel infoPanel;

    void Start()
    {
        loader = GetComponent<IDialogueLoader>();
    }
    
    #region DialoguePanel

    public DialoguePanel StartDialogue(string groupID)
    {
        return ShowDialoguePanel().AddDialogueEvent(loader, groupID).ShowNextDialogueEvent();
    }

    public DialoguePanel StartDialogue(string groupID, Canvas canvas)
    {
        return ShowDialoguePanel(canvas).AddDialogueEvent(loader,groupID).ShowNextDialogueEvent();
    }

    public DialoguePanel ShowDialoguePanel()
    {
        ShowDialoguePanel(FindObjectOfType<Canvas>());

        return dialoguePanel;
    }

    public DialoguePanel ShowDialoguePanel(Canvas canvas)
    {
        DialoguePanel panel = Instantiate(PanelPrefab, canvas.transform);
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localScale = Vector3.one;

        panel.SetTextPanelPosition(TextPanelPosition.DOWN);

        panel.transform.SetAsLastSibling();

        dialoguePanel = panel;
        

        return dialoguePanel;
    }

    #endregion

    #region CursorInfoPanel

    public void ShowCursorInfo(string text)
    {
        ShowCursorInfo(text, FindAnyObjectByType<Canvas>());
    }

    public void ShowCursorInfo(string text, Canvas canvas)
    {
        if (infoPanel is null)
        {
            CursorInfoPanel iPanel = Instantiate(InfoPanelPrefab, canvas.transform);
            iPanel.SetText(text);
            infoPanel = iPanel;
        }
        else
        {
            infoPanel.gameObject.SetActive(true);
            infoPanel.SetText(text);
        }
        Cursor.visible = false;
    }

    public void HideCursorInfo()
    {
        infoPanel.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    #endregion

    public void ShowManual()
    {
        FindAnyObjectByType<GameSettingsPanel>().ShowTerms();
    }
}

public interface IDialogueLoader
{
    public DialogueEvent LoadDialogueEvent(int ID);

    public List<DialogueEvent> LoadDialogueEventsByGroup(string groupID);
}
