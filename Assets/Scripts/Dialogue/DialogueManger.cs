using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField] DialoguePanel PanelPrefab;

    public IDialogueLoader loader;

    DialoguePanel dialoguePanel;

    void Start()
    {
        loader = GetComponent<IDialogueLoader>();
    }

    public void StartDialogue(string groupID)
    {
        ShowDialoguePanel().AddDialogueEvent(loader,groupID).ShowNextDialogueEvent();
    }

    public void StartDialogue(string groupID, Canvas canvas)
    {
        ShowDialoguePanel(canvas).AddDialogueEvent(loader,groupID).ShowNextDialogueEvent();
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
