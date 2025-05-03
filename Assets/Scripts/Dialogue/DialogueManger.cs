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

    public DialoguePanel ShowDialoguePanel()
    {
        DialoguePanel panel = Instantiate(PanelPrefab, FindObjectOfType<Canvas>().transform);
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
