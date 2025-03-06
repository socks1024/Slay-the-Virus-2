using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleEnemyShow : MonoBehaviour
{
    /// <summary>
    /// 敌人引用
    /// </summary>
    EnemyBehaviour simpleEnemy;

    /// <summary>
    /// 敌人图片
    /// </summary>
    Image image;

    void Start()
    {
        image.sprite = simpleEnemy.sprite;
    }

}
