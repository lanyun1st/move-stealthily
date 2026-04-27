using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossController boss;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.ActivateBoss();
            gameObject.SetActive(false);
        }
    }
}