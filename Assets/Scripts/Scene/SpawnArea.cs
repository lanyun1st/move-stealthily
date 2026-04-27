using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Vector3 GetRandomPoint()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        Vector3 center = box.bounds.center;
        Vector3 size = box.bounds.size;

        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(x, center.y, z);
    }
}