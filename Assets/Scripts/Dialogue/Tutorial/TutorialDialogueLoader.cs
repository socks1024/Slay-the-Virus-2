using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour, IDialogueLoader
{
    public TutorialLines tutorialLines;

    public DialogueEvent LoadDialogueEvent(int ID)
    {
        DialogueEvent dialogueEvent = new DialogueEvent();

        foreach (TutorialData data in tutorialLines.Sheet1)
        {
            if (data.ID == ID)
            {
                dialogueEvent.name = data.Name;
                dialogueEvent.line = data.Line;

                return dialogueEvent;
            }
        }

        Debug.LogAssertion("error id");

        return dialogueEvent;
    }

    
}
