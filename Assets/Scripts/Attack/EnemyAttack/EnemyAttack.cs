using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    Transform player;

    public float attackCooldown = 2f;
    public float attackPause = 0.25f;

    private bool attacking = false;
    private float lastAttackTime;

    private NavMeshAgent agent;
    private EnemyAttackType attackType;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attackType = GetComponent<EnemyAttackType>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }

    public void Attack()
    {
        if (player == null) return;
        if (attacking) return;
        if (Time.time < lastAttackTime + attackCooldown) return;

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        attacking = true;
        lastAttackTime = Time.time;

        if (agent != null)
            agent.isStopped = true;

        yield return new WaitForSeconds(attackPause);

        if (attackType != null)
            attackType.DoAttack(player);

        if (agent != null)
            agent.isStopped = false;

        attacking = false;
    }
}