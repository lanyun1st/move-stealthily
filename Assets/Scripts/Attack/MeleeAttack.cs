using UnityEngine;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour, IAttack
{
    public float range = 2f;
    public int damage = 50;
    public float cooldown = 1f;

    public GameObject slashEffect;
    public Transform attackPoint;

    public float noiseOuter = 10f;   // ÍâÈ¦Ìý¼û
    public float noiseInner = 5f;    // ÄÚÈ¦¾¯¾õ

    public void Execute(Transform attacker)
    {
        if (slashEffect != null && attackPoint != null)
        {
            GameObject effect = Instantiate(
                slashEffect,
                attackPoint.position,
                attackPoint.rotation
            );

            Destroy(effect, 1f);
        }

        Collider[] hits = Physics.OverlapSphere(
            attacker.position + attacker.forward,
            range
        );

        HashSet<Health> damagedTargets = new HashSet<Health>();

        foreach (var h in hits)
        {
            if (h.transform == attacker) continue;

            if (h.CompareTag("Enemy") || h.CompareTag("Player"))
            {
                Health hp = h.GetComponent<Health>();

                if (hp != null && !damagedTargets.Contains(hp))
                {
                    hp.TakeDamage(damage);
                    damagedTargets.Add(hp);
                }
            }
        }

        NoiseSystem.Instance.MakeNoise(
        attacker.position,
        noiseOuter,
        noiseInner
);
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (transform != null)
        {
            Gizmos.DrawWireSphere(
                transform.position + transform.forward,
                range
            );
        }
    }


}