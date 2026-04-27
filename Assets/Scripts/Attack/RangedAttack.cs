using UnityEngine;

public class RangedAttack : MonoBehaviour, IAttack
{
    public Transform firePoint;
    public float cooldown = 1.5f;

    public float noiseOuter = 15f;
    public float noiseInner = 8f;

    public void Execute(Transform attacker)
    {
        GameObject obj = PoolManager.Instance.Get("Bullet");

        obj.transform.position = firePoint.position;
        obj.transform.rotation = firePoint.rotation;

        obj.GetComponent<Projectile>()
            .Launch(attacker.forward, attacker.gameObject);

        NoiseSystem.Instance?.MakeNoise(
            attacker.position,
            noiseOuter,
            noiseInner
        );
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}