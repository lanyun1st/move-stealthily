using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float throwForce = 10f;
    public float explodeDelay = 2f;
    public float explodeRadius = 4f;
    public int damage = 30;

    private Rigidbody rb;
    private GameObject owner;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 dir, GameObject attacker)
    {
        Debug.Log("1");
        owner = attacker;
        rb.velocity = dir * throwForce + Vector3.up * 5f;
        Invoke(nameof(Explode), explodeDelay);
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explodeRadius);
        Debug.Log("2");
        foreach (var hit in hits)
        {
            Debug.Log("3");
            if (hit.gameObject == owner) continue;

            if (hit.CompareTag("Player"))
            {
                Debug.Log("4");
                hit.GetComponent<Health>()?.TakeDamage(damage);
            }
        }

        PoolManager.Instance.Return("Bomb", gameObject);
    }
}