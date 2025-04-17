using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupUI : MonoBehaviour
{
    [SerializeField] Transform BossPos;
    [SerializeField] Transform UpRow;
    [SerializeField] Transform DownRow;

    public void OnEnemyAmountChange(List<EnemyBehaviour> enemies)
    {
        if (enemies.Count > 0)
        {
            enemies[0].transform.SetParent(BossPos, false);
        }

        if (enemies.Count > 1)
        {
            enemies[1].transform.SetParent(UpRow, false);
        }
        if (enemies.Count > 2)
        {
            enemies[2].transform.SetParent(UpRow, false);
        }
        
        if (enemies.Count > 3)
        {
            enemies[3].transform.SetParent(DownRow, false);
        }
        if (enemies.Count > 4)
        {
            enemies[4].transform.SetParent(DownRow, false);
        }
    }
}
