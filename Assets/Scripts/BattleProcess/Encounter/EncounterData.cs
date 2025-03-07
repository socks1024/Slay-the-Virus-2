using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterData : ScriptableObject
{
    public List<GameObject> enemies;

    public void InitializeEncounter()
    {
        enemies.ForEach(e => {BattleManager.Instance.enemyGroup.AddEnemyToBattle(e.GetComponent<EnemyBehaviour>(),0);});
    }
}
