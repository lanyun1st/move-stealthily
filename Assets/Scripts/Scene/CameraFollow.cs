using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;             // 玩家
    public Vector3 offset = new Vector3(0, 5, -7); // 固定偏移位置
    public float smoothTime = 0.2f;      // 平滑时间

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // 目标位置 = 玩家位置 + 偏移
        Vector3 targetPosition = target.position + offset;

        // 平滑移动摄像机到目标位置
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // 保持固定方向，不旋转摄像机
        // 如果你想让摄像机始终看向玩家，可以加：
        // transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}