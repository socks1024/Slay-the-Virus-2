using System;
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

                bool hasPanelPicture = false;

                if (headPictures.ContainsKey(data.Name)) dialogueEvent.head = headPictures[data.Name];
                
                if (panelPictures.ContainsKey(data.PanelID))
                {
                    dialogueEvent.panel = panelPictures[data.PanelID];
                    hasPanelPicture = true;
                }

                switch (data.PanelPosition)
                {
                    case "默认":
                        dialogueEvent.textPanelPosition = TextPanelPosition.DEFAULT;
                        break;
                    case "上":
                        dialogueEvent.textPanelPosition = TextPanelPosition.UP;
                        break;
                    case "中":
                        dialogueEvent.textPanelPosition = TextPanelPosition.MIDDLE;
                        break;
                    case "下":
                        dialogueEvent.textPanelPosition = TextPanelPosition.DOWN;
                        break;
                    case "隐藏":
                        dialogueEvent.textPanelPosition = TextPanelPosition.HIDE;
                        break;
                    default:
                        if (hasPanelPicture)
                        {
                            dialogueEvent.textPanelPosition = TextPanelPosition.HIDE;
                        }
                        else
                        {
                            dialogueEvent.textPanelPosition = TextPanelPosition.DEFAULT;
                        }
                        break;
                }

                return dialogueEvent;
            }
        }

        Debug.LogAssertion("error id");

        return dialogueEvent;
    }

    public List<DialogueEvent> LoadDialogueEventsByGroup(string groupID)
    {
        List<DialogueEvent> dialogueEventList = new();

        foreach (TutorialData data in tutorialLines.Sheet1)
        {
            if (data.GroupID == groupID)
            {
                dialogueEventList.Add(LoadDialogueEvent(data.ID));
            }
        }

        return dialogueEventList;
    }
}
