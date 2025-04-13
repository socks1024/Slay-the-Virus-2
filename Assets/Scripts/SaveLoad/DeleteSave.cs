using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSave : MonoBehaviour
{
    
    public Button Confirm;
    public Button Cancel;
    public int index = 0;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Delete()
    {
        SaveSystem.Instance.DeleteSlot(index);
        gameObject.SetActive(false);
        GameObject.Find("StartPanel").GetComponent<StartPanel>().updateslots();
    }

    public void Quit()
    {
        index = 0;
        gameObject.SetActive(false);
    }
}
