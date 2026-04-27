using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject prefab;
    public int count;
}

public class Room : MonoBehaviour
{
    public Transform[] patrolPoints;
    public EnemySpawnInfo[] enemies;

    public bool isActivated = false;

    // ✅新增：存“生成出来的敌人实例”
    public List<EnemyAI> enemyList = new List<EnemyAI>();
}