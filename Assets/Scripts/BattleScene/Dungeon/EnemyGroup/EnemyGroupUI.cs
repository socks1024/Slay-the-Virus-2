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
            enemies[0].transform.localPosition = Vector3.zero;
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

    [Header("敌人通常动画")]
    [SerializeField] float Amplitude = 1f;

    Vector3 uprowBasePos;
    Vector3 bossBasePos;
    Vector3 downrowBasePos;

    void Start()
    {
        uprowBasePos = UpRow.position;
        bossBasePos = BossPos.position;
        downrowBasePos = DownRow.position;
    }

    void Update()
    {
        UpRow.position = new Vector3(uprowBasePos.x, uprowBasePos.y + Mathf.Sin(Time.time) * Amplitude, uprowBasePos.z);
        BossPos.position = new Vector3(bossBasePos.x, bossBasePos.y - Mathf.Sin(Time.time) * Amplitude, bossBasePos.z);
        DownRow.position = new Vector3(downrowBasePos.x, downrowBasePos.y + Mathf.Sin(Time.time) * Amplitude, downrowBasePos.z);
    }

}
