using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NoiseVfx : MonoBehaviour
{
    public LineRenderer outerLine;
    public LineRenderer innerLine;

    public int segments = 80;
    public float duration = 0.4f;

    float outerRadius;
    float innerRadius;

    float timer;

    Material outerMat;
    Material innerMat;

    public void Init(float outer, float inner)
    {
        outerRadius = outer;
        innerRadius = inner;
    }

    void Start()
    {
        Setup(outerLine);
        Setup(innerLine);

        outerMat = outerLine.material;
        innerMat = innerLine.material;

        DrawCircle(outerLine, outerRadius);
        DrawCircle(innerLine, innerRadius);
    }

    void Setup(LineRenderer lr)
    {
        lr.positionCount = segments + 1;
        lr.loop = true;
        lr.useWorldSpace = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        float alpha = Mathf.Lerp(1f, 0f, t);

        SetAlpha(outerMat, alpha);
        SetAlpha(innerMat, alpha);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    void DrawCircle(LineRenderer lr, float radius)
    {
        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            lr.SetPosition(i, new Vector3(x, 0.5f, z));
        }
    }

    void SetAlpha(Material mat, float a)
    {
        if (mat == null) return;

        Color c = mat.color;
        c.a = a;
        mat.color = c;
    }
}