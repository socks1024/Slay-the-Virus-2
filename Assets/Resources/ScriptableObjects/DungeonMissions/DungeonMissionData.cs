using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonMissionData", menuName = "ScriptableObject/DungeonMissionData", order = 0)]
public class DungeonMissionData : ScriptableObject
{
    public string ID;

    [Header("任务UI数据")]

    public string DungeonName;

    public Sprite EnemyImage;

    [TextArea] public string EnemyDescription;

    [Range(1,3)] public int RiskLevel;

    [TextArea] public string Tips;

}
