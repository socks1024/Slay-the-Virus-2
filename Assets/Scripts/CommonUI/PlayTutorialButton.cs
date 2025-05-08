using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTutorialButton : MonoBehaviour
{
    public string tutorialname;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayTutorial);
    }

    public void PlayTutorial()
    {
        DialogueManager.Instance.ShowDialoguePanel(transform.GetComponentInParent<Canvas>()).AddDialogueEvent(DialogueManager.Instance.loader, tutorialname).ShowNextDialogueEvent();
    }
}
