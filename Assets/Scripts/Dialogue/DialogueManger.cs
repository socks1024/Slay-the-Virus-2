using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    public DialoguePanel TutorialPanelPrefab;

    public IDialogueLoader loader;

    void Start()
    {
        loader = GetComponent<IDialogueLoader>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            
            DialogueManager.Instance.ShowDialoguePanel(TextPanelPosition.DOWN)
                .SetDialogueText(loader.LoadDialogue(1))
                .SetOnClick(()=>{ Debug.Log("ShowPanel"); });
            
        }
    }

    public DialoguePanel ShowDialoguePanel(TextPanelPosition position)
    {
        DialoguePanel panel = Instantiate(TutorialPanelPrefab, FindObjectOfType<Canvas>().transform);
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localScale = Vector3.one;

        panel.SetTextPanelPosition(position);

        panel.transform.SetAsLastSibling();

        return panel;
    }
}

public interface IDialogueLoader
{
    public string LoadDialogue(int ID);

    public string LoadDialogue(string name);
}
