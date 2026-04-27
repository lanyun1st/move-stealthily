using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public float viewDistance = 8f;
    public float viewAngle = 60f;
    public float chaseTime = 5f;

    public float stopDistance = 1f;
    public float attackDistance = 2f;

    public Transform[] patrolPoints;

    private Transform player;
    private NavMeshAgent agent;

    private int patrolIndex = 0;
    private float patrolWaitTime = 1f;

    private float chaseTimer = 0f;

    public bool canEngagePlayer = true;

    public Room spawnRoom;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // 👇永远巡逻（不管开没开门）
        Patrol();

        // ❗只有开门后才允许“升级行为”
        if (canEngagePlayer)
        {
            if (CanSeePlayer())
            {
                chaseTimer = chaseTime;
            }

            if (chaseTimer > 0)
            {
                chaseTimer -= Time.deltaTime;
                ChasePlayer();
                TryAttack();
            }
        }
    }

    public void Activate(bool a)
    {
        canEngagePlayer = a;
    }

    bool CanSeePlayer()
    {
        Vector3 dir = player.position - transform.position;

        if (dir.magnitude > viewDistance)
            return false;

        float angle = Vector3.Angle(transform.forward, dir);
        if (angle > viewAngle * 0.5f)
            return false;

        Vector3 eyePos = transform.position + Vector3.up * 1.6f;

        if (Physics.Raycast(eyePos, dir.normalized, out RaycastHit hit, viewDistance))
        {
            return hit.transform.CompareTag("Player") ||
                   hit.transform.root.CompareTag("Player");
        }

        return false;
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        Transform target = patrolPoints[patrolIndex];

        agent.SetDestination(target.position);

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist < 1f)
        {
            patrolWaitTime -= Time.deltaTime;

            if (patrolWaitTime <= 0f)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                patrolWaitTime = 1f;
            }
        }
        else
        {
            patrolWaitTime = 1f;
        }
    }

    void ChasePlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > stopDistance)
            agent.SetDestination(player.position);
        else
            agent.ResetPath();
    }

    void TryAttack()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackDistance)
        {
            GetComponent<EnemyAttack>()?.Attack();
        }
    }

    public void HearNoise(Vector3 noisePos, bool alert)
    {
        if (!canEngagePlayer) return;

        if (alert)
        {
            chaseTimer = chaseTime;
        }
    }
}