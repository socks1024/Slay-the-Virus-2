using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyBuffNextTurnBuff : BuffBehaviour
{
    [HideInInspector]public string newBuffID;

    [HideInInspector]public CreatureBehaviour source;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetBuffNextTurn()
    {
        BuffBehaviour buffPrefab = DungeonBuffLib.buffPrefabs[newBuffID];
        GetComponent<Image>().sprite = buffPrefab.GetComponent<Image>().sprite;
        Type = buffPrefab.Type;
    }

    public override void ActOnLateTurnEnd()
    {
        base.ActOnLateTurnEnd();
        ActionLib.ApplyBuffAction(Owner, source, newBuffID, Amount);
        Amount = 0;
    }
}
