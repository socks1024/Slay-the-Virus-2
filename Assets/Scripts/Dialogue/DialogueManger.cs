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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueManager.Instance.ShowDialoguePanel()
                .AddDialogueEvent(DialogueManager.Instance.loader, new int[]{1,2})
                .ShowNextDialogueEvent();
        }
    }

    public DialoguePanel ShowDialoguePanel()
    {
        if (!dialoguePanel)
        {
            DialoguePanel panel = Instantiate(PanelPrefab, FindObjectOfType<Canvas>().transform);
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localScale = Vector3.one;

            panel.SetTextPanelPosition(TextPanelPosition.DOWN);

            panel.transform.SetAsLastSibling();

            dialoguePanel = panel;
        }

        return dialoguePanel;
    }
}

public interface IDialogueLoader
{
    public DialogueEvent LoadDialogueEvent(int ID);
}
