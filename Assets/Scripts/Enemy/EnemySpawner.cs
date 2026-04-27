using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnRooms;
    public float spawnRadius = 8f;

    void Start()
    {
        SpawnAll();
    }

    void SpawnAll()
    {
        foreach (Transform t in spawnRooms)
        {
            Room room = t.GetComponent<Room>();
            if (room == null) continue;

            foreach (EnemySpawnInfo info in room.enemies)
            {
                for (int i = 0; i < info.count; i++)
                {
                    Spawn(info.prefab, t, room);
                }
            }
        }
    }

    void Spawn(GameObject prefab, Transform roomTf, Room room)
    {
        Vector3 pos = roomTf.position + Random.insideUnitSphere * spawnRadius;
        pos.y = roomTf.position.y;

        GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);

        EnemyAI ai = enemy.GetComponent<EnemyAI>();

        if (ai != null)
        {
            ai.patrolPoints = room.patrolPoints;

            // ✅关键：绑定房间
            ai.spawnRoom = room;

            // ✅关键：加入房间列表
            room.enemyList.Add(ai);

            // ✅初始状态
            ai.canEngagePlayer = room.isActivated;
        }
    }
}