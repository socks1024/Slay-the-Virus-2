using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShow : MonoBehaviour
{
    EnemyBehaviour enemyBehaviour;

    RectTransform rect;

    [Header("敌人显示设置")]
    [SerializeField] int BossSize;
    [SerializeField] int NormalSize;

    void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        rect = GetComponent<RectTransform>();
        
        switch (enemyBehaviour.type)
        {
            case EnemyType.BOSS:
                rect.sizeDelta = new Vector2(BossSize, BossSize);
                break;
            case EnemyType.ELITE:
                break;
            case EnemyType.NORMAL:
                rect.sizeDelta = new Vector2(NormalSize, NormalSize);
                break;
            default:
                Debug.LogError("enemy no type");
                break;
        }
    }
}
