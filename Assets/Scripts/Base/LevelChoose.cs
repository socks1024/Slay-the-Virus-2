using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChoose : MonoBehaviour
{
    public CameraControl cameraControl;
    public GameObject[] RedDots;

    private bool EnterTutorial;

    public bool Levelchoose = false;

    private void Start()
    {
        for(int i = 0; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(false);
        }

        EnterTutorial = SaveSystem.Instance.getSave().TutorialClear[2];
    }

    public void StartLevelChoose()
    {
        Levelchoose = true;

        RedDots[0].gameObject.SetActive(true);

        bool[] levelclear = SaveSystem.Instance.getSave().ClearLevels;

        if (EnterTutorial)
        {
            RedDots[1].gameObject.SetActive(true);
        }

        for (int i = 2; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(levelclear[i - 2]);
        }

        cameraControl.StartFocus();
    }
    public void ReturnMain()
    {
        for (int i = 0; i < RedDots.Length; i++)
        {
            RedDots[i].gameObject.SetActive(false);
        }
    }

}
