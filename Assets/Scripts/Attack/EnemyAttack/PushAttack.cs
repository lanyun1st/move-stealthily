using UnityEngine;

public class PushAttack : EnemyAttackType
{
    public float pushForce = 6f;
    public int damage = 10;

    public override void DoAttack(Transform player)
    {
        Vector3 dir = (player.position - transform.position).normalized;

        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.AddKnockback(dir * pushForce);
        }

        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}