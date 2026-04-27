using UnityEngine;

public class UICheck : MonoBehaviour
{
    public UIHintManager ui;

    public Transform player;
    public Transform door;

    public bool showDoorHint = true;
    public bool needClearEnemy = true;

    public float interactDistance = 3f;

    private bool enemyCleared = false;
    private bool zoneEntered = false;

    void Update()
    {
        if (needClearEnemy)
        {
            CheckEnemies();
        }
        else
        {
            enemyCleared = true;
        }

        if (showDoorHint)
        {
            CheckDoor();
        }
    }

    void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (!enemyCleared && enemies.Length == 0)
        {
            enemyCleared = true;
            ui.Show("The area has been cleared");
        }
    }

    void CheckDoor()
    {
        if (zoneEntered) return;
        if (!enemyCleared) return;

        float dist = Vector3.Distance(player.position, door.position);

        if (dist <= interactDistance)
        {
            ui.Show("Press E to open the door");

            if (Input.GetKeyDown(KeyCode.E))
            {
                zoneEntered = true;
                // 这里你再去调用你的 ExitDoor 开门逻辑
            }
        }
    }
}