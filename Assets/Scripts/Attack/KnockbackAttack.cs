using UnityEngine;

public class KnockbackAttack : IAttack
{
    int damage = 10;
    float cooldown = 2f;
    float knockbackForce = 5f;

    public void Execute(Transform attacker)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        Vector3 dir = (player.transform.position - attacker.position).normalized;

        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(dir * knockbackForce, ForceMode.Impulse);
        }
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}