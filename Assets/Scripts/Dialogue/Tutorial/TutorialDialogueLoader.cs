using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour, IDialogueLoader
{
    [SerializeField] TutorialLines tutorialLines;

    [SerializeField] SerializableDictionary<string, Sprite> headPictures;

    public DialogueEvent LoadDialogueEvent(int ID)
    {
        DialogueEvent dialogueEvent = new DialogueEvent();

        foreach (TutorialData data in tutorialLines.Sheet1)
        {
            if (data.ID == ID)
            {
                dialogueEvent.name = data.Name;
                dialogueEvent.line = data.Line;

                if (headPictures.ContainsKey(data.Name)) dialogueEvent.sprite = headPictures[data.Name];

                return dialogueEvent;
            }
        }

        Debug.LogAssertion("error id");

        return dialogueEvent;
    }

    
}
