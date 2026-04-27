using UnityEngine;

public class EnemyBombAttack : EnemyAttackType
{
    public Transform throwPoint;

    public override void DoAttack(Transform player)
    {
        GameObject obj = PoolManager.Instance.Get("Bomb");

        obj.transform.position = throwPoint.position;
        obj.transform.rotation = throwPoint.rotation;

        if (player == null) return;

        Vector3 dir = (player.position - throwPoint.position).normalized;

        obj.GetComponent<Bomb>().Launch(dir, gameObject);
    }
}