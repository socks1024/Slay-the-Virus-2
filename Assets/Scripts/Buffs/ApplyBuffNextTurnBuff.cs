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
        BuffBehaviour buff = DungeonBuffLib.GetBuff(newBuffID, 1);
        GetComponent<Image>().sprite = buff.GetComponent<Image>().sprite;
        Name = buff.Name + "（下回合）";
        Type = buff.Type;
    }

    public override void ActOnTurnEnd()
    {
        base.ActOnTurnEnd();
        ActionLib.ApplyBuffAction(Owner, source, newBuffID, Amount);
    }
}
