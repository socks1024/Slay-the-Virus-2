using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    
    [SerializeField]
    private GameObject exitpanel;
    [SerializeField]
    private GameObject grouppanel;
    [SerializeField]
    private GameObject settingpanel;
    [SerializeField]
    private GameObject mainpanel;
    [SerializeField]
    private GameObject startpanel;

    private void Start()
    {
        mainpanel.SetActive(true);
        exitpanel.SetActive(false);
        grouppanel.SetActive(false);
        settingpanel.SetActive(false);
        startpanel.SetActive(false);
    }

    public void OnStartGame()
    {
        startpanel.SetActive(true);
        mainpanel.SetActive(false);
    }

    public void OnOpenSettings()
    {
        settingpanel.SetActive(true);
        
    }

    public void OnReturnToMain()
    {
        settingpanel.SetActive(false);
        startpanel.SetActive(false);
        mainpanel.SetActive(true);
    }

    public void OnExitGame()
    {
        exitpanel.SetActive(true);
        //Debug.Log("exit");
    }

    public void OnCloseExitGame()
    {
        exitpanel.SetActive(false);
    }
   public void OnGruop()
    {
        grouppanel.SetActive(true);
    }

    public void OnExitGroup()
    {
        grouppanel.SetActive(false);
    }
}
