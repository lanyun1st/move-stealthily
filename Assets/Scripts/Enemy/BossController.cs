using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;

    public EnemyRangedAttack rangedAttack;
    public EnemyBombAttack bombAttack;

    public float moveSpeed = 3f;
    public float moveRange = 8f;

    public float shootDuration = 3f;
    public float shootInterval = 0.2f;

    public int bombCount = 3;

    private bool activated = false;

    private float shootTimer;
    private float shootIntervalTimer;

    private Vector3 targetPos;
    private Vector3 originPos;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        originPos = transform.position;   // 记录出生点
        targetPos = originPos;
    }

    void Update()
    {
        if (!activated) return;

        FacePlayer();
        MoveRandom();

        shootTimer += Time.deltaTime;
        shootIntervalTimer += Time.deltaTime;

        if (shootTimer < shootDuration)
        {
            if (shootIntervalTimer >= shootInterval)
            {
                rangedAttack.DoAttack(player);
                shootIntervalTimer = 0f;
            }
        }
        else
        {
            for (int i = 0; i < bombCount; i++)
            {
                bombAttack.DoAttack(player);
            }

            shootTimer = 0f;
        }
    }

    void FacePlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        transform.forward = dir.normalized;
    }

    void MoveRandom()
    {
        if (Vector3.Distance(transform.position, targetPos) < 1f)
        {
            Vector2 random = Random.insideUnitCircle * moveRange;

            targetPos = new Vector3(
                originPos.x + random.x,
                transform.position.y,
                originPos.z + random.y
            );
        }

        Vector3 dir = (targetPos - transform.position).normalized;

        controller.Move(dir * moveSpeed * Time.deltaTime);
    }

    public void ActivateBoss()
    {
        activated = true;
    }
}