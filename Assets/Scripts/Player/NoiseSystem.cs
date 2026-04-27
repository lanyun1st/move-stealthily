using UnityEngine;

public class NoiseSystem : MonoBehaviour
{
    public static NoiseSystem Instance;

    public GameObject noiseVfxPrefab; // 关键：拖预制体

    void Awake()
    {
        Instance = this;
    }

    public void MakeNoise(Vector3 pos, float outerRadius, float innerRadius)
    {
        // 1. 生成视觉效果
        if (noiseVfxPrefab != null)
        {
            GameObject vfx = Instantiate(noiseVfxPrefab, pos, Quaternion.identity);
            vfx.GetComponent<NoiseVfx>().Init(outerRadius, innerRadius);
        }

        // 2. AI感知逻辑（你原来的）
        Collider[] hits = Physics.OverlapSphere(pos, outerRadius);

        foreach (var h in hits)
        {
            EnemyAI ai = h.GetComponent<EnemyAI>();
            if (ai == null) continue;

            float dist = Vector3.Distance(pos, h.transform.position);

            if (dist <= innerRadius)
            {
                ai.HearNoise(pos, true);
            }
            else
            {
                ai.HearNoise(pos, false);
            }
        }
    }
}