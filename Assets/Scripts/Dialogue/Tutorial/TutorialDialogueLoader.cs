using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour, IDialogueLoader
{
    [SerializeField] TutorialLines tutorialLines;

    [SerializeField] SerializableDictionary<string, Sprite> headPictures;

    [SerializeField] SerializableDictionary<string, Sprite> panelPictures;

    public DialogueEvent LoadDialogueEvent(int ID)
    {
        DialogueEvent dialogueEvent = new DialogueEvent();

        foreach (TutorialData data in tutorialLines.Sheet1)
        {
            if (data.ID == ID)
            {
                dialogueEvent.name = data.Name;
                dialogueEvent.line = data.Line;

                if (headPictures.ContainsKey(data.Name)) dialogueEvent.head = headPictures[data.Name];
                if (panelPictures.ContainsKey(data.Name))
                {
                    dialogueEvent.panel = panelPictures[data.Name];
                    dialogueEvent.textPanelPosition = TextPanelPosition.HIDE;
                } 

                return dialogueEvent;
            }
        }

        Debug.LogAssertion("error id");

        return dialogueEvent;
    }

    
}
