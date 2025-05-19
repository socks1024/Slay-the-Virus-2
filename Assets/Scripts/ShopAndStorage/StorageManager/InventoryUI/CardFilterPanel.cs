using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFilterPanel : MonoBehaviour
{
    public Toggle[] toggles;
    public InitItems initItems;

    public void ReInit()
    {
        bool[] bools = new bool[9];

        for(int i = 0; i < 9; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        //initItems.SetCardSelector(bools);

        initItems.ReInitCards(bools);

        gameObject.SetActive(false);
    }
}
