using UnityEngine;

public class MaxHealthPowerUp : MonoBehaviour
{
    public int increaseAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.AddMaxHealth(increaseAmount);
            Destroy(gameObject);
        }
    }
}