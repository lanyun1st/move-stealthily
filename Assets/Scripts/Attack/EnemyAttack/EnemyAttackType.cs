using UnityEngine;

public abstract class EnemyAttackType : MonoBehaviour
{
    public abstract void DoAttack(Transform player);
}