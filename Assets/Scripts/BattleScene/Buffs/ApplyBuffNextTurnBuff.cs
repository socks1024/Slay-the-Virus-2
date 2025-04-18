using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyBuffNextTurnBuff : BuffBehaviour
{
    [HideInInspector]public string newBuffID;

    [HideInInspector]public CreatureBehaviour source;

    protected void Start()
    {
        BuffBehaviour buffPrefab = DungeonBuffLib.buffPrefabs[newBuffID];
        GetComponent<Image>().sprite = buffPrefab.GetComponent<Image>().sprite;
        Name = buffPrefab.Name + "（下回合）";
        Type = buffPrefab.Type;
    }

    public override void ActOnTurnEnd()
    {
        base.ActOnTurnEnd();
        ActionLib.ApplyBuffAction(Owner, source, newBuffID, Amount);
        Amount = 0;
    }
}
