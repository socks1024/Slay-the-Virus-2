using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFilterPanel : MonoBehaviour
{
    public Toggle[] toggles;
    public InitItems initItems;

    public void ReInitByPack()
    {
        bool[] bools = new bool[9];

        for(int i = 0; i < 9; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        //initItems.SetCardSelector(bools);

        initItems.ReInitCardsByPack(bools);

        gameObject.SetActive(false);
    }


    public void ReInitByRarity()
    {
        bool[] bools = new bool[3];

        for (int i = 0; i < 3; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        initItems.ReInitCardsByRarity(bools);

        gameObject.SetActive(false);
    }

    public void ReInitByType()
    {
        bool[] bools = new bool[5];

        for(int i = 0; i < 5; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        initItems.ReInitCardByType(bools);
        gameObject.SetActive(false);
    }
}
