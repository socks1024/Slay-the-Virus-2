using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleEnemyData", menuName = "ScriptableObject/CreatureData/SimpleEnemyData", order = 0)]
public class SimpleEnemyData : CreatureData
{
    /// <summary>
    /// 敌人的图像
    /// </summary>
    public Sprite EnemySprite;
}
