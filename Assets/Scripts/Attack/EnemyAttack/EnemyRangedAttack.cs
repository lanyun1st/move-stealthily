using UnityEngine;

public class EnemyRangedAttack : EnemyAttackType
{
    public Transform firePoint;

    public override void DoAttack(Transform player)
    {
        GameObject obj = PoolManager.Instance.Get("Bullet");

        obj.transform.position = firePoint.position;
        obj.transform.rotation = firePoint.rotation;

        if (player == null) return;

        // ❗关键：始终指向玩家（水平面）
        Vector3 targetPos = player.position;
        targetPos.y = firePoint.position.y;

        Vector3 dir = (targetPos - firePoint.position).normalized;

        // ❗可选：让子弹也朝这个方向（更稳定）
        obj.transform.forward = dir;

        obj.GetComponent<Projectile>()
            .Launch(dir, gameObject);
    }
}