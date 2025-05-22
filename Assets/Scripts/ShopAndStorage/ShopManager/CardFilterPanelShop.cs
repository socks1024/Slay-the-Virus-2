using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFilterPanelShop : MonoBehaviour
{
    public Toggle[] toggles;
    public ShopManager shopManager;

    public void ReInitByPack()
    {
        bool[] bools = new bool[8];

        for (int i = 0; i < 8; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        //initItems.SetCardSelector(bools);

        shopManager.ReInitCardsByPack(bools);

        gameObject.SetActive(false);
    }

    public void ReInitByRarity()
    {
        bool[] bools = new bool[2];

        for (int i = 0; i < 2; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        //initItems.SetCardSelector(bools);

        shopManager.ReInitCardsByRarity(bools);

        gameObject.SetActive(false);
    }

    public void ReInitByType()
    {
        bool[] bools = new bool[5];

        for (int i = 0; i < 5; i++)
        {
            bools[i] = toggles[i].isOn;
        }

        //initItems.SetCardSelector(bools);

        shopManager.ReInitCardsByType(bools);

        gameObject.SetActive(false);
    }
}
