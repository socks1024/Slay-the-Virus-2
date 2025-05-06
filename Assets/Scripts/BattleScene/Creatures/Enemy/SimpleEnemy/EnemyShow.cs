using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        GetComponent<Image>().sprite = enemyBehaviour.enemySprite;
        
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

    [Header("高亮/聚焦")]
    public float FocusScale = 1.2f;

    public void EnterHighlight()
    {
        // switch (enemyBehaviour.type)
        // {
        //     case EnemyType.BOSS:
        //         rect.sizeDelta = new Vector2(BossSize * FocusScale, BossSize * FocusScale);
        //         break;
        //     case EnemyType.ELITE:
        //         break;
        //     case EnemyType.NORMAL:
        //         rect.sizeDelta = new Vector2(NormalSize * FocusScale, NormalSize * FocusScale);
        //         break;
        //     default:
        //         Debug.LogError("enemy no type");
        //         break;
        // }

        rect.localScale *= FocusScale;
    }

    public void ExitHighlight()
    {
        // switch (enemyBehaviour.type)
        // {
        //     case EnemyType.BOSS:
        //         rect.sizeDelta = new Vector2(BossSize, BossSize);
        //         break;
        //     case EnemyType.ELITE:
        //         break;
        //     case EnemyType.NORMAL:
        //         rect.sizeDelta = new Vector2(NormalSize, NormalSize);
        //         break;
        //     default:
        //         Debug.LogError("enemy no type");
        //         break;
        // }

        rect.localScale = Vector3.one;
    }
}
