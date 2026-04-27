using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int size = 10;

        public Queue<GameObject> objects = new Queue<GameObject>();
    }

    public List<Pool> pools;

    void Awake()
    {
        Instance = this;

        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pool.objects.Enqueue(obj);
            }
        }
    }

    public GameObject Get(string key)
    {
        Pool pool = pools.Find(p => p.key == key);

        if (pool == null)
        {
            Debug.LogError("Pool not found: " + key);
            return null;
        }

        if (pool.objects.Count == 0)
        {
            GameObject obj = Instantiate(pool.prefab);
            return obj;
        }

        GameObject obj2 = pool.objects.Dequeue();
        obj2.SetActive(true);
        return obj2;
    }

    public void Return(string key, GameObject obj)
    {
        obj.SetActive(false);

        Pool pool = pools.Find(p => p.key == key);

        if (pool != null)
        {
            pool.objects.Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}