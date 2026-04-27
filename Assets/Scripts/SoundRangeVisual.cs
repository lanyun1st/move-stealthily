using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SoundRangeVisual : MonoBehaviour
{
    public float radius = 10f;
    public int segments = 60;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.positionCount = segments + 1;
        line.loop = true;
        line.widthMultiplier = 0.05f;

        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, 1.0f, z));

            angle += 360f / segments;
        }
    }
}