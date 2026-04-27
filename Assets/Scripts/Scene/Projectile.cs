using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 3f;

    private Vector3 direction;
    private GameObject owner;

    private float timer;
    private bool initialized = false;

    public void Launch(Vector3 dir, GameObject attacker)
    {
        direction = dir.normalized;
        owner = attacker;
        timer = lifeTime;

        initialized = true;

        transform.forward = direction;
    }

    void OnEnable()
    {
        timer = lifeTime;
        direction = Vector3.zero;
        owner = null;
        initialized = false;
    }

    void Update()
    {
        if (!initialized) return;

        transform.position += direction * speed * Time.deltaTime;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ReturnToPool();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!initialized) return;

        if (other.gameObject == owner) return;

        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            other.GetComponent<Health>()?.TakeDamage(damage);
            ReturnToPool();
            return;
        }

        if (other.CompareTag("Wall"))
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        initialized = false;
        PoolManager.Instance.Return("Bullet", gameObject);
    }
}