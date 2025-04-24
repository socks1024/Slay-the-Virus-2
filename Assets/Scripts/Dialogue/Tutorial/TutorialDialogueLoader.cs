using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour, IDialogueLoader
{
    public TutorialLines tutorialLines;

    public string LoadDialogue(int ID)
    {
        foreach (TutorialData data in tutorialLines.Sheet1)
        {
            if (data.ID == ID)
            {
                return data.Line;
            }
        }

        return "no such line";
    }

    public string LoadDialogue(string name)
    {
        foreach (TutorialData data in tutorialLines.Sheet1)
        {
            if (data.Name == name)
            {
                return data.Line;
            }
        }

        return "no such line";
    }
}
